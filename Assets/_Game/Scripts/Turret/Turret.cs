using GameTick;
using System;
using UnityEngine;
using Zenject;
using ITickable = GameTick.ITickable;

namespace Gameplay
{
    public class Turret : ITickable, IDisposable
    {
        private TurretView _view;

        private TurretRotationController _rotationController;
        private TurretFiringController _firingController;

        private ITurretInput _input;
        private GameTickProvider _tickProvider;

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

            _rotationController = view.Get<TurretRotationController>();
            _firingController = view.Get<TurretFiringController>();
        }

        public void Tick(float deltaTime)
        {
            if (!_input.IsActive()) return;

            var inputAxes = _input.GetAxes();
            _rotationController.Rotate(inputAxes, deltaTime);

            _firingController.TryFire();
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
    }
}