using Infrastructure.Services.Random;
using Infrastructure.Services.SceneLoad;
using Infrastructure.Services.StaticData;
using Zenject;

namespace Infrastructure.Installer
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneService();
            BindStaticDataService();
            BindRandomService();

        }

        private void BindRandomService() =>
            Container
                .Bind<IRandomService>()
                .To<RandomService>()
                .AsSingle();

        private void BindSceneService()
        {
            Container
                .Bind<ISceneLoadService>()
                .To<SceneLoadService>()
                .AsSingle();
        }

        private void BindStaticDataService()
        {
            Container
                .BindInterfacesAndSelfTo<StaticDataService>()
                .AsSingle();
        }
    }
}