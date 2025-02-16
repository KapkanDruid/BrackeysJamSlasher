using Assets.Scripts.Architecture;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerJumpHandler : ITickable, IDisposable
    {
        private readonly PlayerData _playerData;
        private readonly Transform _viewObjectTransform;
        private readonly InputSystemActions _inputActions;

        private float _jumpHeight;
        private float _jumpDuration;
        private float _jumpTimer;

        private bool _isJumping;
        private bool _isGrounded;

        private Vector3 _jumpStartPosition;

        public bool IsGrounded => _isGrounded;

        public PlayerJumpHandler(PlayerData playerData, InputSystemActions inputActions)
        {
            _playerData = playerData;
            _inputActions = inputActions;

            _viewObjectTransform = _playerData.ViewObjectTransform;

            _isGrounded = true;

            _inputActions.Player.Jump.performed += OnInputJump;
        }

        private void OnInputJump(InputAction.CallbackContext context)
        {
            if (!_isGrounded)
                return;

            _isGrounded = false;

            _isJumping = true;
            _jumpTimer = 0;

            _jumpHeight = _playerData.JumpHeight;
            _jumpDuration = _playerData.JumpDuration;

            _jumpStartPosition = _viewObjectTransform.localPosition;
        }

        public void Tick()
        {
            if (!_isJumping)
                return;

            _jumpTimer += Time.deltaTime;

            float t = _jumpTimer / _jumpDuration;
            float heightOffset = _jumpHeight * (1 - 4 * Mathf.Pow(t - 0.5f, 2));

            _viewObjectTransform.localPosition = new Vector3(_jumpStartPosition.x, _jumpStartPosition.y + heightOffset, _jumpStartPosition.z);

            if (_jumpTimer >= _jumpDuration)
            {
                _isJumping = false;
                _isGrounded = true;

                _viewObjectTransform.localPosition = _jumpStartPosition;
            }
        }

        public void Dispose()
        {
            _inputActions.Player.Jump.performed -= OnInputJump;
        }
    }
}
