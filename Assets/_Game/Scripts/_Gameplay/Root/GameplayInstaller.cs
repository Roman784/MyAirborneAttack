using Gameplay;
using System;
using UnityEngine;
using Zenject;

namespace GameplayRoot
{
    public sealed class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactories();
            BindTurretInput();
        }

        private void BindFactories()
        {
            Container.Bind<GameplayLevelFactory>().AsSingle();
            Container.Bind<ProjectileFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<TurretFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
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
    }
}