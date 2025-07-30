using GameRoot;
using System;
using System.Collections;
using UnityEngine;

namespace GameplayRoot
{
    public sealed class GameplayEntryPoint : SceneEntryPoint
    {
        public override IEnumerator Run<T>(T enterParams)
        {
            if (enterParams is GameplayEnterParams gameplayParams)
                yield return Run(gameplayParams);
            else
                throw new ArgumentException($"Failed to convert {typeof(T)} to {typeof(GameplayEnterParams)}!");
        }

        private IEnumerator Run(GameplayEnterParams enterParams)
        {
            var isLoaded = false;

            

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}