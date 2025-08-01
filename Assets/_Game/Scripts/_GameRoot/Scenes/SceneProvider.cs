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

        public void OpenGameplay(int levelNumber, string turretId)
        {
            var enterParams = new GameplayEnterParams(levelNumber, turretId);
            _sceneLoader.LoadAndStartGameplay(enterParams);
        }
    }
}