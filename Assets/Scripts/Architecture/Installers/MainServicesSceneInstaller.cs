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
        [SerializeField] private SceneResources _sceneResources;
        [SerializeField] private MainSceneBootstrap _sceneBootstrap;
        [SerializeField] private Canvas _levelCanvas;
        [SerializeField] private SceneTransiter _sceneTransiter;

        public override void InstallBindings()
        {
            Container.Bind<InputSystemActions>().AsSingle().NonLazy();
            Container.Bind<PlayerController>().FromInstance(_player).AsSingle();
            Container.Bind<MainSceneBootstrap>().FromInstance(_sceneBootstrap).AsSingle();
            Container.Bind<GroundDirectionPointsHandler>().FromInstance(_directionPointsHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<GroundDirectionFinder>().AsSingle().NonLazy();
            Container.Bind<SceneResources>().FromInstance(_sceneResources).AsSingle();
            Container.Bind<PopupTextController>().AsSingle().NonLazy();

            Container.Bind<SceneTransiter>().FromInstance(_sceneTransiter).AsSingle().NonLazy();
            Container.Bind<Canvas>().FromInstance(_levelCanvas).AsSingle().NonLazy();

            Container.Bind<GameEndController>().AsSingle().NonLazy();
        }
    }
}