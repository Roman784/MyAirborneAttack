using System.Collections;
using UnityEngine;
using Utils;
using R3;
using System;

namespace GameRoot
{
    public sealed class GameEntryPoint : SceneEntryPoint
    {
        private void Start()
        {
            var enterParams = new SceneEnterParams(Scenes.BOOT);
            Coroutines.Start(Run(enterParams));
        }

        public override IEnumerator Run<T>(T _)
        {
            SetAppSettings();

            yield return LoadGameConfig();

            StartGame();
        }

        private void SetAppSettings()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private IEnumerator LoadGameConfig()
        {
            var isLoaded = false;
            _configProvider.LoadGameConfig().Subscribe(result =>
            {
                if (result)
                    isLoaded = true;
                else
                    throw new Exception("Failed to load the game config!");
            });
            yield return new WaitUntil(() => isLoaded);
        }

        // Starts the first scene the player will see.
        private void StartGame()
        {
            _sceneProvider.OpenGameplay(1, "Turret");
        }
    }
}