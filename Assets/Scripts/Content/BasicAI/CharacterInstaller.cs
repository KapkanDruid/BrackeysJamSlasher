using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private CharacterHandler _characterHandler;

        public override void InstallBindings()
        {
            Container.Bind<CharacterHandler>().FromComponentOnRoot().AsSingle().NonLazy();
            Container.Bind<CharacterData>().FromInstance(_characterHandler.CharacterDatas).AsSingle();
            Container.Bind<CharacterHealthHandler>().AsSingle();
            Container.Bind<CharacterStateMachine>().FromComponentOnRoot().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterSensor>().AsSingle().NonLazy();
        }
    }
}