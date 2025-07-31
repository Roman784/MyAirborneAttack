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

        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private float _fireRate;
        [SerializeField] private float _projectileInitialFlightSpeed;
        [SerializeField] private float _projectileDamage;

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
                _nextTimeToFire = Time.time + 1f / _fireRate;
                _shootingStrategy.Shot(_projectilePrefab, _projectileInitialFlightSpeed, _projectileDamage);
            }
        }

        private void OnDrawGizmos()
        {
            _shootingStrategy.DrawTrajectory(_projectileInitialFlightSpeed);
        }
    }
}