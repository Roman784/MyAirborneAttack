using Effects;
using Gameplay;
using System;
using UI;
using UnityEngine;
using Zenject;

namespace GameplayRoot
{
    public sealed class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameOverPopUp _gameOverPopUpPrefab;
        [SerializeField] private LevelPassedPopUp _levelPassedPopUpPrefab;

        public override void InstallBindings()
        {
            BindFactories();
            BindTurretInput();
            BindPopUps();
        }

        private void BindFactories()
        {
            Container.Bind<SceneUIFactory>().AsSingle();
            Container.Bind<GameplayLevelFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProjectileFactory>().AsSingle();
            Container.Bind<TurretFactory>().AsSingle();
            Container.Bind<EnemyFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EffectsFacotry>().AsSingle();
        }

        private void BindTurretInput()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop || Application.isEditor)
            {
                Container.Bind<ITurretInput>().To<KeyboardTurretInput>().AsTransient();
            }
            else if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                Container.Bind<ITurretInput>().To<TouchTurretInput>().AsTransient();
            }
            else
            {
                throw new Exception("Failed to recognize the platform for control input!");
            }
        }

        private void BindPopUps()
        {
            Container.Bind<GameplayPopUpProvider>().AsTransient();

            Container.BindFactory<GameOverPopUp, GameOverPopUp.Factory>()
                .FromComponentInNewPrefab(_gameOverPopUpPrefab);
            Container.BindFactory<LevelPassedPopUp, LevelPassedPopUp.Factory>()
                .FromComponentInNewPrefab(_levelPassedPopUpPrefab);
        }
    }
}