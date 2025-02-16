using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content
{
    public class CharacterJumpHandler : ITickable
    {
        private readonly IJumpData _jumpData;
        private readonly Transform _viewObjectTransform;

        private float _jumpHeight;
        private float _jumpDuration;
        private float _jumpTimer;

        private bool _isJumping;
        private bool _isGrounded;

        private Vector3 _jumpStartPosition;
        private Vector3 _shadowSize;

        public bool IsGrounded => _isGrounded;

        public CharacterJumpHandler(IJumpData playerData)
        {
            _jumpData = playerData;

            _viewObjectTransform = _jumpData.ViewObjectTransform;

            _isGrounded = true;
        }

        public void Jump()
        {
            if (!_isGrounded)
                return;

            _isGrounded = false;

            _isJumping = true;
            _jumpTimer = 0;

            _jumpHeight = _jumpData.JumpHeight;
            _jumpDuration = _jumpData.JumpDuration;

            _jumpStartPosition = _viewObjectTransform.localPosition;
            _shadowSize = _jumpData.ShadowTransform.localScale;
        }

        public void Tick()
        {
            if (!_isJumping)
                return;

            _jumpTimer += Time.deltaTime;

            float t = _jumpTimer / _jumpDuration;
            float heightOffset = _jumpHeight * (1 - 4 * Mathf.Pow(t - 0.5f, 2));

            _viewObjectTransform.localPosition = new Vector3(_jumpStartPosition.x, _jumpStartPosition.y + heightOffset, _jumpStartPosition.z);

            _jumpData.ShadowTransform.localScale = ParabolicLerp(_shadowSize, _shadowSize / 1.3f, t);

            if (_jumpTimer >= _jumpDuration)
            {
                _isJumping = false;
                _isGrounded = true;

                _viewObjectTransform.localPosition = _jumpStartPosition;
            }
        }

        private Vector2 ParabolicLerp(Vector2 A, Vector2 B, float t)
        {
            float factor = 4 * t * (1 - t);
            return Vector2.Lerp(A, B, factor);
        }
    }
}
