using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class TurretFiringController : MonoBehaviour
    {
        [SerializeField] private Shooting _shootingStrategy;

        [Space]

        [SerializeField] private ShootingData _shootingData;

        private float _nextTimeToShoot;

        public void TryFire()
        {
            if (Time.time >= _nextTimeToShoot)
            {
                _nextTimeToShoot = Time.time + 1f / _shootingData.Rate;
                _shootingStrategy.Shoot(_shootingData);
            }
        }

        // Trajectory.
        private void OnDrawGizmos()
        {
            var socket = GetComponentsInChildren<Transform>(includeInactive: true)
                .FirstOrDefault(t => t.name == "ProjectileSocket") ?? transform;

            if (_shootingData.ProjectileType == ProjectileType.Parabolic)
                ParabolicProjectile.DrawTrajectory(socket, _shootingData.ProjectileFlightSpeed);
        }
    }
}