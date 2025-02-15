using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerInputHandler>().AsSingle();
        }
    }
}