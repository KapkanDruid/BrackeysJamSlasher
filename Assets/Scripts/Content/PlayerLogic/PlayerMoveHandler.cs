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
        private readonly InputSystemActions _inputActions;
        private readonly Rigidbody2D _rigidbody;
        private readonly PlayerData _playerData;

        private Vector2 _movementVelocity;
        private Vector2 _inputVector;

        public PlayerMoveHandler
            (InputSystemActions inputActions,
            GroundDirectionFinder groundDirectionFinder,
            PlayerData playerData,
            Rigidbody2D rigidbody)
        {
            _groundDirectionFinder = groundDirectionFinder;
            _inputActions = inputActions;
            _playerData = playerData;
            _rigidbody = rigidbody;

            _inputActions.Player.Move.performed += OnInputVectorChanged;
            _inputActions.Player.Move.canceled += OnInputVectorChanged;
        }

        private void OnInputVectorChanged(InputAction.CallbackContext context)
        {
            _inputVector = context.ReadValue<Vector2>();
        }

        public void FixedTick()
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
        }

        public void OnDrawGizmos()
        {
            if (_movementVelocity == Vector2.zero)
                return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(_rigidbody.transform.position, _movementVelocity);
        }
    }

    public class PLayerAttackHandler
    {

    }
}
