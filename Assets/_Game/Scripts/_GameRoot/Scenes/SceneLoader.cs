using GameplayRoot;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace GameRoot
{
    public class SceneLoader
    {
        private readonly UIRoot _uiRoot;

        private Coroutine _loading;

        public SceneLoader(UIRoot uiRoot)
        {
            _uiRoot = uiRoot;
        }

        public void LoadAndStartGameplay(GameplayEnterParams enterParams)
        {
            StopLoading();
            _loading = Coroutines.Start(
                LoadAndRunScene<GameplayEntryPoint, GameplayEnterParams>(Scenes.GAMEPLAY, enterParams));
        }

        private IEnumerator LoadAndRunScene<TEntryPoint, TEnterParams>(string sceneName, TEnterParams enterParams)
            where TEntryPoint : SceneEntryPoint
            where TEnterParams : SceneEnterParams
        {
            yield return _uiRoot.ShowLoadingScreen();
            _uiRoot.ClearAllContainers();

            yield return LoadScene(sceneName);

            var sceneEntryPoint = Object.FindObjectOfType<TEntryPoint>();
            yield return sceneEntryPoint.Run(enterParams);

            yield return _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return null;
        }

        private void StopLoading()
        {
            Coroutines.Stop(_loading);
        }
    }
}