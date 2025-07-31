using Assets;
using Configs;
using Zenject;

namespace GameRoot
{
    public sealed class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindProviders();
        }

        private void BindProviders()
        {
            Container.Bind<SceneProvider>().AsSingle();
            Container.Bind<IConfigProvider>().To<ScriptableObjectConfigProvider>().AsSingle();
            Container.Bind<IAssetsProvider>().To<ResourcesAssetsProvider>().AsSingle();
        }
    }
}