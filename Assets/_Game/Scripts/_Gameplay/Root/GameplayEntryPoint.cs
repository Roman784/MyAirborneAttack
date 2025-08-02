using Configs;
using Gameplay;
using GameRoot;
using System;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;
using R3;

namespace GameplayRoot
{
    public sealed class GameplayEntryPoint : SceneEntryPoint
    {
        [SerializeField] private GameplayUI _gameplayUIPrefab;

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

            // Data.
            var levelNumber = enterParams.LevelNumber;
            var levelConfig = LevelsConfig.GetLevelConfig(levelNumber);

            // Level setup.
            var level = _levelFactory.Create(levelConfig);
            var turret = level.CreateTurret(enterParams.TurretNameId);
            level.PrepareEnemies(turret);
            var startWaveSignal = level.PrepareWaves();

            // UI.
            var ui = _uiFactory.Create(_gameplayUIPrefab);
            startWaveSignal.Subscribe(e => ui.ShowWaveProgress(e.Item1, e.Item2));

            // Start gameplay.
            Observable.Timer(TimeSpan.FromSeconds(1))
                .Subscribe(_ => level.StartWaves());

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}