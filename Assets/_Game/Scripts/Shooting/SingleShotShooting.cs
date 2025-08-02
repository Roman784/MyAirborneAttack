using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class SingleShotShooting : Shooting
    {
        [Space]

        [SerializeField] private Transform _projectileSocket; // Point of projectile exit.

        protected override void Shoot(ShootingData shootingData)
        {
            var projectileFlightDirection = -_projectileSocket.transform.right;

            if (shootingData.ProjectileType == ProjectileType.Parabolic)
                _projectileFactory.CreateParabolic(shootingData, _projectileSocket.position, projectileFlightDirection);
        }
    }
}