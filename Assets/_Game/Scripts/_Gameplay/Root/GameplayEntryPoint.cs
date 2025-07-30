using Configs;
using GameplayLevel;
using GameRoot;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace GameplayRoot
{
    public sealed class GameplayEntryPoint : SceneEntryPoint
    {
        private GameplayLevelFactory _levelFactory;

        private GameplayLevelsConfig LevelsConfig => GameConfig.LevelsConfig;

        [Inject]
        private void Construct(GameplayLevelFactory levelFactory)
        {
            _levelFactory = levelFactory;
        }

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

            var levelNumber = enterParams.LevelNumber;
            var levelConfig = LevelsConfig.GetLevelConfig(levelNumber);

            _levelFactory.Create(levelConfig.NameId);

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}