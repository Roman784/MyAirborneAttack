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
        private GameplayPopUpProvider _popUpProvider;

        private GameplayLevelsConfig LevelsConfig => GameConfig.LevelsConfig;

        [Inject]
        private void Construct(GameplayLevelFactory levelFactory, GameplayPopUpProvider popUpProvider)
        {
            _levelFactory = levelFactory;
            _popUpProvider = popUpProvider;
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
            ui.InitTurrentHealthBar(turret);

            turret.OnDeathSignal
                .Subscribe(_ => Observable.Timer(TimeSpan.FromSeconds(1))
                .Subscribe(_ => _popUpProvider.OpenGameOverPopUp()));

            level.LevelPassedSignal
                .Subscribe(_ => Observable.Timer(TimeSpan.FromSeconds(1))
                .Subscribe(_ => _popUpProvider.OpenLevelPassedPopUp()));

            // Start gameplay.
            Observable.Timer(TimeSpan.FromSeconds(1))
                .Subscribe(_ => level.StartWaves());

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}