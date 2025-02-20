using Assets.Scripts.Architecture;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public sealed class PlayerController : MonoBehaviour, IEntity
    {
        [SerializeField] private PlayerData _playerData;

        private PlayerHealthHandler _healthHandler;
        private CharacterJumpHandler _jumpHandler;
        private InputSystemActions _inputActions;
        private Animator _animator;

        public PlayerData PlayerData => _playerData;

        [Inject]
        private void Construct(
            CharacterJumpHandler jumpHandler, 
            InputSystemActions inputActions, 
            Animator animator,
            PlayerHealthHandler healthHandler)
        {
            _healthHandler = healthHandler;
            _inputActions = inputActions;
            _jumpHandler = jumpHandler;
            _animator = animator;

            _playerData.ThisEntity = this;
            _playerData.CancellationToken = this.GetCancellationTokenOnDestroy();
        }

        private void Update()
        {
            if (_animator != null) 
            {
                _animator.SetBool(AnimatorHashes.IsGrounded, _jumpHandler.IsGrounded);
            }
        }

        public T ProvideComponent<T>() where T : class
        {
            if (_playerData.Flags is T flags)
                return flags;

            if (_healthHandler is T healthHandler)
                return healthHandler;
            
            if (_playerData.PlayerTransform is T transform)
                return transform;


            return null;
        }
    }
}