using Effects;
using R3;
using System;
using UnityEngine;
using Zenject;
using ITickable = GameTick.ITickable;

namespace Gameplay
{
    public abstract class Projectile : ITickable, IDisposable
    {
        private const float LIFESPAN = 3f;

        protected ProjectileView _view;

        protected float _flightTime;
        protected Vector3 _initialPosition;
        protected ShootingData _shootingData;
        protected float _gravity;

        private Subject<RaycastHit> _onHitSignalSubj;
        private Subject<Unit> _lifeOverSignalSubj;

        private bool _isEnabled;

        private EffectsFacotry _effectsFacotry;

        public Observable<RaycastHit> OnHitSignal => _onHitSignalSubj;
        public Observable<Unit> LifeOverSignal => _lifeOverSignalSubj;

        [Inject]
        private void Construct(EffectsFacotry effectsFacotry)
        {
            _effectsFacotry = effectsFacotry;
        }

        public Projectile(ProjectileView view, ShootingData shootingData, float gravity)
        {
            _view = view;
            _isEnabled = true;

            _initialPosition = _view.Position;
            _shootingData = shootingData;
            _gravity = gravity;

            _onHitSignalSubj = new Subject<RaycastHit>();
            _lifeOverSignalSubj = new Subject<Unit>();
        }

        public void Tick(float deltaTime)
        {
            if (!_isEnabled) return;

            _flightTime += deltaTime;

            if (!CheckLifespan()) return;
            if (CheckCollisions()) return;

            Move();
            RotateAccordingMovement();
        }

        public void Dispose()
        {
            _isEnabled = false;
            _lifeOverSignalSubj.OnNext(Unit.Default);
            _lifeOverSignalSubj.OnCompleted();
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
            if (Physics.Raycast(_view.PreviousPosition, distanceVector, out RaycastHit hit, distanceVector.magnitude, _shootingData.TargetLayer))
            {
                if (hit.collider.TryGetComponent<Hitbox>(out var hitbox))
                    hitbox.Hit(hit, _shootingData.Damage);

                CreateHitEffect(hit);

                _onHitSignalSubj.OnNext(hit);
                _onHitSignalSubj.OnCompleted();

                return true;
            }
            return false;
        }

        private void CreateHitEffect(RaycastHit hit)
        {
            _effectsFacotry.Create(_shootingData.HitEffectPrefab, hit.point, hit.normal);
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