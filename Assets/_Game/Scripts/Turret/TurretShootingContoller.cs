using System;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class TurretShootingContoller : MonoBehaviour
    {
        [SerializeField] protected ShootingData _shootingData;

        private Shooting _shooting;

        private void Awake()
        {
            _shooting = GetComponent<Shooting>();

            if (_shooting == null)
                throw new NullReferenceException("Shooting component not found!");

            _shooting.Init(_shootingData);
        }

        public void Shoot()
        {
            _shooting.TryShoot();
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