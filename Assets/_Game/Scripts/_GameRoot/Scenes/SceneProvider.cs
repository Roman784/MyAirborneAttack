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
            Debug.Log("Load gameplay");
        }
    }
}