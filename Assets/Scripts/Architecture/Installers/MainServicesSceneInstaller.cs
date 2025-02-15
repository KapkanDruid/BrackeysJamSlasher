using Zenject;

namespace Assets.Scripts.Architecture
{
    public class MainServicesSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<InputSystemActions>().AsSingle();
        }
    }
}