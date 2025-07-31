using R3;
using UnityEngine;
using ITickable = GameTick.ITickable;

namespace Gameplay
{
    public abstract class Projectile : ITickable
    {
        protected const float GRAVITY = -9.8f;
        private const float LIFESPAN = 3f;

        protected ProjectileView _view;

        protected float _flightTime;
        protected Vector3 _initialPosition;
        protected ShootingData _shootingData;

        private Subject<RaycastHit> _onHitSignalSubj;
        private Subject<Unit> _lifeOverSignalSubj;

        public Observable<RaycastHit> OnHitSignal => _onHitSignalSubj;
        public Observable<Unit> LifeOverSignal => _lifeOverSignalSubj;

        public Projectile(ProjectileView view, ShootingData shootingData)
        {
            _view = view;

            _initialPosition = _view.Position;
            _shootingData = shootingData;

            _onHitSignalSubj = new Subject<RaycastHit>();
            _lifeOverSignalSubj = new Subject<Unit>();
        }

        public void Tick(float deltaTime)
        {
            _flightTime += deltaTime;

            if (!CheckLifespan()) return;
            if (CheckCollisions()) return;

            Move();
            RotateAccordingMovement();
        }

        protected abstract void Move();

        private void RotateAccordingMovement()
        {
            var flightDirection = _view.Position - _view.PreviousPosition;
            if (flightDirection == Vector3.zero) return;

            _view.Rotate(Quaternion.LookRotation(flightDirection));
        }

        private bool CheckCollisions()
        {
            var distanceVector = _view.Position - _view.PreviousPosition;
            if (Physics.Raycast(_view.PreviousPosition, distanceVector, out RaycastHit hit, _shootingData.TargetLayer))
            {
                _onHitSignalSubj.OnNext(hit);
                _onHitSignalSubj.OnCompleted();

                return true;
            }
            return false;
        }

        private bool CheckLifespan()
        {
            if (_flightTime >= LIFESPAN)
            {
                _lifeOverSignalSubj.OnNext(Unit.Default);
                _lifeOverSignalSubj.OnCompleted();

                return false;
            }
            return true;
        }
    }
}