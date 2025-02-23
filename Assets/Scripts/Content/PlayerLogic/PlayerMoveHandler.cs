using Assets.Scripts.Architecture;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public sealed class PlayerMoveHandler : IFixedTickable, IDisposable, IGizmosDrawer
    {
        private readonly GroundDirectionFinder _groundDirectionFinder;
        private readonly PlayerHealthHandler _playerHealthHandler;
        private readonly CharacterJumpHandler _jumpHandler;
        private readonly InputSystemActions _inputActions;
        private readonly AudioController _audioController;
        private readonly Rigidbody2D _rigidbody;
        private readonly PlayerData _playerData;
        private readonly Animator _animator;

        private Vector2 _movementVelocity;
        private Vector2 _inputVector;

        private bool _isLanding;
        private int _currentOrientation;

        public bool IsLanding { get => _isLanding; set => _isLanding = value; }
        public int CurrentOrientation => _currentOrientation;

        public PlayerMoveHandler
            (InputSystemActions inputActions,
            GroundDirectionFinder groundDirectionFinder,
            PlayerData playerData,
            Rigidbody2D rigidbody,
            Animator animator,
            PlayerHealthHandler playerHealthHandler,
            CharacterJumpHandler jumpHandler,
            AudioController controller)
        {
            _groundDirectionFinder = groundDirectionFinder;
            _playerHealthHandler = playerHealthHandler;
            _audioController = controller;
            _inputActions = inputActions;
            _jumpHandler = jumpHandler;
            _playerData = playerData;
            _rigidbody = rigidbody;
            _animator = animator;

            _currentOrientation = Mathf.Clamp(Mathf.RoundToInt(_playerData.PlayerTransform.localScale.x), -1, 1);

            _inputActions.Player.Move.performed += OnInputVectorChanged;
            _inputActions.Player.Move.canceled += OnInputVectorChanged;

            _inputActions.Player.Jump.performed += context => OnInputJump();
        }

        private void OnInputVectorChanged(InputAction.CallbackContext context)
        {
            _inputVector = context.ReadValue<Vector2>();

            if (_inputVector != Vector2.zero)
                _animator.SetBool(AnimatorHashes.IsMoving, true);
            else
                _animator.SetBool(AnimatorHashes.IsMoving, false);
        }

        private void UpdateStepSound()
        {
            if (_inputVector != Vector2.zero && IsMovementAllowed() && _jumpHandler.IsGrounded)
                _audioController.StartFootsteps();
            else
                _audioController.StopFootsteps();
        }

        private void SetOrientation(float direction)
        {
            if (!IsMovementAllowed())
                return;

            Transform playerTransform = _playerData.PlayerTransform;

            Vector3 rightOrientation = new Vector3(1, playerTransform.localScale.y, playerTransform.localScale.z);
            Vector3 leftOrientation = new Vector3(-1, playerTransform.localScale.y, playerTransform.localScale.z);


            if (direction > 0)
            {
                _currentOrientation = 1;
                playerTransform.localScale = rightOrientation;
            }
            else if (direction < 0)
            {
                _currentOrientation = -1;
                playerTransform.localScale = leftOrientation;
            }
        }

        private void OnInputJump()
        {
            if (!IsMovementAllowed())
                return;

            _jumpHandler.Jump();
            _animator.SetTrigger(AnimatorHashes.JumpTrigger);
            _audioController.StopFootsteps();

            UpdateStepSound();
        }

        public void FixedTick()
        {
            SetOrientation(_inputVector.x);

            if (IsMovementAllowed())
                PlayerMovement();
            else
                _rigidbody.linearVelocity = Vector2.zero;

            UpdateStepSound();
        }

        private bool IsMovementAllowed()
        {
            if (_animator.GetBool(AnimatorHashes.IsLanding))
                return false;

            if (_animator.GetBool(AnimatorHashes.IsAttacking))
                return false;

            if (_animator.GetBool(AnimatorHashes.IsTakingDamage))
                return false;

            if (_playerHealthHandler.IsDead)
                return false;

            return true;
        }

        private void PlayerMovement()
        {
            var additionalVelocity = _groundDirectionFinder.GetVectorByPosition(_playerData.PlayerTransform.position);

            additionalVelocity *= _inputVector.x;

            _movementVelocity = new Vector2(additionalVelocity.x, additionalVelocity.y + _inputVector.y).normalized;

            if (_movementVelocity != Vector2.zero)
                _rigidbody.linearVelocity = _movementVelocity * _playerData.Speed;
            else
                _rigidbody.linearVelocity = Vector2.zero;
        }

        public void Dispose()
        {
            _inputActions.Player.Move.performed -= OnInputVectorChanged;
            _inputActions.Player.Move.canceled -= OnInputVectorChanged;

            _inputActions.Player.Jump.performed -= context => OnInputJump();
        }

        public void OnDrawGizmos()
        {
            if (_movementVelocity == Vector2.zero)
                return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(_rigidbody.transform.position, _movementVelocity);
        }
    }
}
