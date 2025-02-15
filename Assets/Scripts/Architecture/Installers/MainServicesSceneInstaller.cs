using Assets.Scripts.Content.PlayerLogic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Architecture
{
    public class MainServicesSceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerController _player;
        public override void InstallBindings()
        {
            Container.Bind<InputSystemActions>().AsSingle();
            Container.Bind<PlayerController>().FromInstance(_player).AsSingle();
        }
    }
}