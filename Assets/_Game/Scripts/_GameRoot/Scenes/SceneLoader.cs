using GameplayRoot;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace GameRoot
{
    public class SceneLoader
    {
        private Coroutine _loading;

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
            yield return LoadScene(sceneName);

            var sceneEntryPoint = Object.FindObjectOfType<TEntryPoint>();
            yield return sceneEntryPoint.Run(enterParams);
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