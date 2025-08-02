using GameplayRoot;
using UI;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public class SceneProvider
    { 
        private readonly SceneLoader _sceneLoader;

        [Inject]
        public SceneProvider(UIRoot uiRoot)
        {
            _sceneLoader = new SceneLoader(uiRoot);
        }

        public void OpenGameplay(int levelNumber, string turretNameId)
        {
            var enterParams = new GameplayEnterParams(levelNumber, turretNameId);
            _sceneLoader.LoadAndStartGameplay(enterParams);
        }
    }
}