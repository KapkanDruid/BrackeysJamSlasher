using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorEventHandler _animatorEventHandler;
        [Inject] private PlayerController _playerController;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CharacterJumpHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerAttackHandler>().AsSingle().NonLazy();
            Container.Bind<PlayerAttackAnimationController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerHealthHandler>().AsSingle().NonLazy();

            Container.Bind<Rigidbody2D>().FromComponentOnRoot().AsSingle();

            Container.Bind<Animator>().FromInstance(_animator).AsSingle();
            Container.Bind<AnimatorEventHandler>().FromInstance(_animatorEventHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerData>().FromInstance(_playerController.PlayerData).AsSingle();
        }
    }
}