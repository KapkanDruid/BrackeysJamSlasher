using Assets.Scripts.Content;
using Assets.Scripts.Content.PlayerLogic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Architecture
{
    public class MainServicesSceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private GroundDirectionPointsHandler _directionPointsHandler;

        public override void InstallBindings()
        {
            Container.Bind<InputSystemActions>().AsSingle();
            Container.Bind<PlayerController>().FromInstance(_player).AsSingle();
            Container.Bind<GroundDirectionPointsHandler>().FromInstance(_directionPointsHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<GroundDirectionFinder>().AsSingle().NonLazy();
        }
    }
}