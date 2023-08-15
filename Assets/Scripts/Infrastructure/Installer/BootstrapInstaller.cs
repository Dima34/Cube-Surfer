using Infrastructure.Services.CoroutineRunner;
using Infrastructure.Services.SceneLoad;
using Infrastructure.Services.StaticData;
using Zenject;

namespace Infrastructure.Installer
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCoroutineRunner();
            BindSceneService();
            BindStaticDataService();
        }

        private void BindCoroutineRunner() =>
            Container
                .BindInterfacesAndSelfTo<CoroutineRunnerService>()
                .FromNewComponentOn(gameObject)
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