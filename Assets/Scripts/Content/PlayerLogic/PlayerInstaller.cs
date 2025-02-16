using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerInstaller : MonoInstaller
    {
        [Inject] private PlayerController playerController;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerJumpHandler>().AsSingle().NonLazy();

            Container.Bind<Rigidbody2D>().FromComponentOnRoot().AsSingle();
            Container.Bind<PlayerData>().FromInstance(playerController.PlayerData).AsSingle();
        }
    }
}