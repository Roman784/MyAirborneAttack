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

        public void OpenGameplay()
        {
            var enterParams = new GameplayEnterParams();
            _sceneLoader.LoadAndStartGameplay(enterParams);
        }
    }
}