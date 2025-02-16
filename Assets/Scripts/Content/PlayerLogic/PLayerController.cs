using Assets.Scripts.Architecture;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public sealed class PlayerController : MonoBehaviour, IEntity
    {
        [SerializeField] private PlayerData _playerData;

        private CharacterJumpHandler _jumpHandler;
        private InputSystemActions _inputActions;

        public PlayerData PlayerData => _playerData;

        [Inject]
        private void Construct(CharacterJumpHandler jumpHandler, InputSystemActions inputActions)
        {
            _jumpHandler = jumpHandler;
            _inputActions = inputActions;

            _inputActions.Player.Jump.performed += OnInputJump;
        }

        private void OnInputJump(InputAction.CallbackContext context)
        {
            _jumpHandler.Jump();
        }

        public T ProvideComponent<T>() where T : class
        {
            if (_playerData.Flags is T flags)
                return flags;

            return null;
        }

        private void OnDestroy()
        {
            _inputActions.Player.Jump.performed -= OnInputJump;
        }
    }
}