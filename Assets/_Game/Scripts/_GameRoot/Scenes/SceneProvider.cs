using GameplayRoot;
using UnityEngine;

namespace GameRoot
{
    public class SceneProvider
    { 
        private readonly SceneLoader _sceneLoader;

        public SceneProvider()
        {
            _sceneLoader = new SceneLoader();
        }

        public void OpenGameplay(int levelNumber, string turretNameId)
        {
            var enterParams = new GameplayEnterParams(levelNumber, turretNameId);
            _sceneLoader.LoadAndStartGameplay(enterParams);
        }
    }
}