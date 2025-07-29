using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace GameRoot
{
    public class SceneLoader
    {
        private Coroutine _loading;

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
        }

        private void StopLoading()
        {
            Coroutines.Stop(_loading);
        }
    }
}