using System.Collections;
using UnityEngine;
using Utils;
using Zenject;

namespace GameRoot
{
    public sealed class GameEntryPoint : SceneEntryPoint
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

            StartGame();
        }

        private void SetAppSettings()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        // Starts the first scene the player will see.
        private void StartGame()
        {
            _sceneProvider.OpenGameplay();
        }
    }
}