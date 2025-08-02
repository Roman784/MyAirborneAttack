using GameTick;
using R3;
using System;
using UnityEngine;
using Zenject;
using ITickable = GameTick.ITickable;

namespace Gameplay
{
    public class Turret : ITickable, IDisposable
    {
        private const float MAX_HEALTH = 100;

        private TurretView _view;
        private Health _health;

        private TurretRotationController _rotationController;
        private TurretShootingContoller _shootingContoller;

        private ITurretInput _input;
        private GameTickProvider _tickProvider;

        public Observable<Unit> OnDeathSignal => _health.OnDeathSignal;

        [Inject]
        public void Construct(ITurretInput input, GameTickProvider tickProvider)
        {
            _input = input;

            _tickProvider = tickProvider;
            _tickProvider.AddTickable(this);
        }

        public Turret(TurretView view)
        {
            _view = view;
            _health = new Health(MAX_HEALTH);

            _view.DamageReceiver.DamageSignal
                .Subscribe(damage => _health.TakeDamage(damage));
            _health.OnDeathSignal.Subscribe(_ => OnDeath());

            _rotationController = view.Get<TurretRotationController>();
            _shootingContoller = view.Get<TurretShootingContoller>();
        }

        public void Tick(float deltaTime)
        {
            if (!_input.IsActive()) return;

            var inputAxes = _input.GetAxes();
            _rotationController.Rotate(inputAxes, deltaTime);

            _shootingContoller.Shoot();
        }

        public void AttachCamera(TrackingCamera camera)
        {
            var initialRotation = new Vector2(0, _rotationController.InitialBarrelAngle);
            camera.Attach(_view.CameraAnchor, _rotationController.AnglesChangedSignal, initialRotation);
        }

        public void Dispose()
        {
            _tickProvider.RemoveTickable(this);
        }

        private void OnDeath()
        {
            _view.Destroy();
            Dispose();
        }
    }
}