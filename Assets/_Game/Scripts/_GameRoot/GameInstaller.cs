using Assets;
using Configs;
using GameTick;
using UnityEngine;
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
            BindTickProvider();
        }

        private void BindTickProvider()
        {
            var tickProvider = new GameObject("[TICK_PROVIDER]").AddComponent<GameTickProvider>();
            DontDestroyOnLoad(tickProvider.gameObject);
            Container.Bind<GameTickProvider>().FromInstance(tickProvider).AsSingle();
        }
    }
}