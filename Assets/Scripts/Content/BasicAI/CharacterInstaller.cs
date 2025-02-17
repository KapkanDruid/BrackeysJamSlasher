using UnityEngine.AI;
using Zenject;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CharacterHandler>().FromComponentOnRoot().AsSingle();
            Container.Bind<CharacterData>().FromMethod(ctx => ctx.Container.Resolve<CharacterHandler>().CharacterData).AsSingle();
            Container.Bind<CharacterHealthHandler>().AsSingle();
            Container.Bind<CharacterStateMachine>().FromComponentOnRoot().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterSensor>().AsSingle().NonLazy();
        }
    }
}