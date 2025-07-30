using GameplayLevel;
using Zenject;

namespace GameplayRoot
{
    public sealed class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactories();
        }

        private void BindFactories()
        {
            Container.Bind<GameplayLevelFactory>().AsSingle();
        }
    }
}