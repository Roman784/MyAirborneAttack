using Assets;
using Configs;
using GameTick;
using UI;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private UIRoot _uiRootPrefab;

        public override void InstallBindings()
        {
            BindProviders();
            BindFactories();
            BindUI();
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

        private void BindFactories()
        {
            Container.Bind<SceneUIFactory>().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<UIRoot>().FromComponentInNewPrefab(_uiRootPrefab).AsSingle().NonLazy();
        }
    }
}