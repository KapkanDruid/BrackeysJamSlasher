using Assets.Scripts.Architecture;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public sealed class PlayerMoveHandler : IFixedTickable, IDisposable
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly InputSystemActions _inputActions;
        private readonly PlayerData _playerData;

        private Vector3 _movementVelocity;

        public PlayerMoveHandler(InputSystemActions inputActions, PlayerData playerData, Rigidbody2D rigidbody)
        {
            _inputActions = inputActions;
            _playerData = playerData;
            _rigidbody = rigidbody;

            _inputActions.Player.Move.performed += OnInputVectorChanged;
            _inputActions.Player.Move.canceled += OnInputVectorChanged;
        }

        private void OnInputVectorChanged(InputAction.CallbackContext context)
        {
            var moveVector = context.ReadValue<Vector2>();

            _movementVelocity.x = moveVector.x;
            _movementVelocity.y = moveVector.y;
        }

        public void FixedTick()
        {
            _rigidbody.linearVelocity = _movementVelocity * _playerData.Speed;
        }

        public void Dispose()
        {
            _inputActions.Player.Move.performed -= OnInputVectorChanged;
            _inputActions.Player.Move.canceled -= OnInputVectorChanged;
        }
    }

    public class PLayerAttackHandler
    {

    }
}
