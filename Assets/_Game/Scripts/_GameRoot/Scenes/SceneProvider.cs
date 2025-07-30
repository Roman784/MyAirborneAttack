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

        public void OpenGameplay(int levelNumber)
        {
            var enterParams = new GameplayEnterParams(levelNumber);
            _sceneLoader.LoadAndStartGameplay(enterParams);
        }
    }
}