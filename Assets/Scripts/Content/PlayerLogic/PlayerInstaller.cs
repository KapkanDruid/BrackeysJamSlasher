using UnityEngine.AI;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerInstaller : MonoInstaller
    {
        [Inject] private PlayerController playerController;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle().NonLazy();
            Container.Bind<NavMeshAgent>().FromComponentOnRoot().AsSingle();
            Container.Bind<PlayerData>().FromInstance(playerController.PlayerData).AsSingle();
        }
    }
}