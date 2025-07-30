using System.Collections;
using UnityEngine;
using Utils;

namespace GameRoot
{
    public class GameEntryPoint : SceneEntryPoint
    {
        private void Start()
        {
            var enterParams = new SceneEnterParams(Scenes.BOOT);
            Coroutines.Start(Run(enterParams));
        }

        public override IEnumerator Run<T>(T enterParams)
        {
            var isLoaded = false;

            SetAppSettings();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);

            LoadScene(enterParams);
        }

        private void LoadScene(SceneEnterParams currentEnterParams)
        {
            Debug.Log("Load gameplay");
        }

        private void SetAppSettings()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}