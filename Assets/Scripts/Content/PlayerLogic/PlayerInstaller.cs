using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Animator _animator;
        [Inject] private PlayerController playerController;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CharacterJumpHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerAttackHandler>().AsSingle().NonLazy();
            Container.Bind<PlayerAttackAnimationController>().AsSingle().NonLazy();

            Container.Bind<Rigidbody2D>().FromComponentOnRoot().AsSingle();
            Container.Bind<Animator>().FromInstance(_animator).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerData>().FromInstance(playerController.PlayerData).AsSingle();
        }
    }
}