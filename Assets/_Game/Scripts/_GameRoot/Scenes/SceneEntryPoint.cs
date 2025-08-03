using Configs;
using GameTick;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public abstract class SceneEntryPoint : MonoBehaviour
    {
        protected SceneUIFactory _uiFactory;
        protected GameTickProvider _gameTickProvider;
        protected SceneProvider _sceneProvider;
        protected IConfigProvider _configProvider;

        protected GameConfig GameConfig => _configProvider.GameConfigs;

        [Inject]
        private void Construct(SceneUIFactory uiFactory, GameTickProvider gameTickProvider,
                               SceneProvider sceneProvider, IConfigProvider configProvider)
        {
            _uiFactory = uiFactory;
            _gameTickProvider = gameTickProvider;
            _sceneProvider = sceneProvider;
            _configProvider = configProvider;
        }

        public abstract IEnumerator Run<T>(T enterParams) where T : SceneEnterParams;
    }
}