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
        private readonly Rigidbody2D _rigidbody;
        private readonly PlayerData _playerData;
        private readonly Animator _animator;

        private Vector2 _movementVelocity;
        private Vector2 _inputVector;

        private bool _isLanding;

        public bool IsLanding { get => _isLanding; set => _isLanding = value; }

        public PlayerMoveHandler
            (InputSystemActions inputActions,
            GroundDirectionFinder groundDirectionFinder,
            PlayerData playerData,
            Rigidbody2D rigidbody,
            Animator animator,
            PlayerHealthHandler playerHealthHandler,
            CharacterJumpHandler jumpHandler)
        {
            _groundDirectionFinder = groundDirectionFinder;
            _playerHealthHandler = playerHealthHandler;
            _inputActions = inputActions;
            _jumpHandler = jumpHandler;
            _playerData = playerData;
            _rigidbody = rigidbody;
            _animator = animator;

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

        private void OnInputJump()
        {
            if (!IsMovementAllowed())
                return;

            _jumpHandler.Jump();
            _animator.SetTrigger(AnimatorHashes.JumpTrigger);
        }

        public void FixedTick()
        {
            if (IsMovementAllowed())
                PlayerMovement();
            else
                _rigidbody.linearVelocity = Vector2.zero;
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
