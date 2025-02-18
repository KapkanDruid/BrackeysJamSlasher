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
        private Animator _animator;

        public PlayerData PlayerData => _playerData;

        [Inject]
        private void Construct(
            CharacterJumpHandler jumpHandler, 
            InputSystemActions inputActions, 
            Animator animator,
            PopupTextController popupTextController)
        {
            _inputActions = inputActions;
            _jumpHandler = jumpHandler;
            _animator = animator;

            _playerData.ThisEntity = this;

            _inputActions.Player.Jump.performed += OnInputJump;

            _inputActions.Player.Attack.performed += context => popupTextController.ShowDamage(_playerData.JumpObjectTransform.position, 10);
        }

        private void OnInputJump(InputAction.CallbackContext context)
        {
            _jumpHandler.Jump();
            _animator.SetTrigger(AnimatorHashes.JumpTrigger);
        }

        public T ProvideComponent<T>() where T : class
        {
            if (_playerData.Flags is T flags)
                return flags;

            return null;
        }

        private void Update()
        {
            if (_animator != null) 
            {
                _animator.SetBool(AnimatorHashes.IsGrounded, _jumpHandler.IsGrounded);
            }
        }

        private void OnDestroy()
        {
            _inputActions.Player.Jump.performed -= OnInputJump;
        }
    }
}