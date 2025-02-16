using Assets.Scripts.Architecture;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public sealed class PlayerMoveHandler : ITickable, IDisposable
    {
        private readonly Transform _playerTransform;
        private readonly InputSystemActions _inputActions;
        private readonly PlayerData _playerData;

        private Vector3 _movementVelocity;

        public PlayerMoveHandler(InputSystemActions inputActions, PlayerData playerData)
        {
            _inputActions = inputActions;
            _playerData = playerData;
            _playerTransform = _playerData.PlayerTransform;

            _inputActions.Player.Move.performed += OnInputVectorChanged;
            _inputActions.Player.Move.canceled += OnInputVectorChanged;
        }

        private void OnInputVectorChanged(InputAction.CallbackContext context)
        {
            var moveVector = context.ReadValue<Vector2>();

            _movementVelocity.x = moveVector.x;
            _movementVelocity.z = moveVector.y;
        }

        public void Tick()
        {
            _playerTransform.position += _movementVelocity * _playerData.Speed * Time.deltaTime;
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
