using GameTick;
using UnityEngine;
using Zenject;
using ITickable = GameTick.ITickable;

namespace Gameplay
{
    public class TurretFiringController : MonoBehaviour, ITickable
    {
        [SerializeField] private Shooting _shootingStrategy;

        [Space]

        [SerializeField] private ShootingData _shootingData;

        private float _nextTimeToFire;

        private ITurretInput _input;

        [Inject]
        private void Construct(ITurretInput input, GameTickProvider gameTickProvider)
        {
            _input = input;

            gameTickProvider.AddTickable(this);
        }

        public void Tick(float deltaTime)
        {
            if (!_input.IsActive()) return;

            if (Time.time >= _nextTimeToFire)
            {
                _nextTimeToFire = Time.time + 1f / _shootingData.Rate;
                _shootingStrategy.Shoot(_shootingData);
            }
        }

        private void OnDrawGizmos()
        {
            if (_shootingData.ProjectileType == ProjectileType.Parabolic)
                ParabolicProjectile.DrawTrajectory(transform, _shootingData.ProjectileFlightSpeed);
        }
    }
}