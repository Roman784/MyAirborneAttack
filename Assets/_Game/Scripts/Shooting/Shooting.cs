using System.Linq;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public abstract class Shooting : MonoBehaviour
    {
        protected ShootingData _shootingData;
        protected ProjectileFactory _projectileFactory;
        
        private float _nextTimeToShoot;

        [Inject]
        private void Construct(ProjectileFactory projectileFactory)
        {
            _projectileFactory = projectileFactory;
        }

        public void Init(ShootingData shootingData)
        {
            _shootingData = shootingData;
        }

        public bool TryShoot()
        {
            if (Time.time >= _nextTimeToShoot)
            {
                _nextTimeToShoot = Time.time + 1f / _shootingData.Rate;
                Shoot(_shootingData);
                
                return true;
            }
            return false;
        }

        public virtual void TakeAim(Transform target) { }
        protected abstract void Shoot(ShootingData shootingData);

        protected Projectile CreateProjectile(ShootingData shootingData, Transform socket)
        {
            var projectileFlightDirection = socket.forward;

            switch (shootingData.ProjectileType)
            {
                case ProjectileType.Parabolic:
                    return _projectileFactory.CreateParabolic(shootingData, socket.position, projectileFlightDirection);
                case ProjectileType.Straight:
                    return _projectileFactory.CreateStraight(shootingData, socket.position, projectileFlightDirection);
            }

            throw new System.Exception("Projectile type not identified!");
        }

        // Trajectory.
        private void OnDrawGizmos()
        {
            var socket = GetComponentsInChildren<Transform>(includeInactive: true)
                .FirstOrDefault(t => t.name == "ProjectileSocket") ?? transform;

            switch (_shootingData?.ProjectileType)
            {
                case ProjectileType.Parabolic:
                    ParabolicProjectile.DrawTrajectory(socket, _shootingData.ProjectileFlightSpeed);
                    break;
                case ProjectileType.Straight:
                    StraightProjectile.DrawTrajectory(socket, _shootingData.ProjectileFlightSpeed);
                    break;
            }
        }
    }
}