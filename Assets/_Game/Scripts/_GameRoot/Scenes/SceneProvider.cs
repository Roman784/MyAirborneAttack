using GameplayRoot;
using UI;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public class SceneProvider
    { 
        private readonly SceneLoader _sceneLoader;

        private string _currentSceneName;
        private SceneEnterParams _currentSceneEnterParams;

        [Inject]
        public SceneProvider(UIRoot uiRoot)
        {
            _sceneLoader = new SceneLoader(uiRoot);
        }

        public void OpenGameplay(int levelNumber, string turretNameId)
        {
            var enterParams = new GameplayEnterParams(levelNumber, turretNameId);

            _currentSceneName = Scenes.GAMEPLAY;
            _currentSceneEnterParams = enterParams;
            
            _sceneLoader.LoadAndStartGameplay(enterParams);
        }

        public void TryRestartScene()
        {
            if (_currentSceneEnterParams == null) return;

            switch (_currentSceneName)
            {
                case Scenes.GAMEPLAY:
                    _sceneLoader.LoadAndStartGameplay((GameplayEnterParams)_currentSceneEnterParams);
                    break;
            }
        }
    }
}