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
        }
    }
}