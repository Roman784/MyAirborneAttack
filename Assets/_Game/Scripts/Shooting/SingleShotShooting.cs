using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class SingleShotShooting : Shooting
    {
        [Space]

        [SerializeField] private Transform _projectileSocket; // Point of projectile exit.

        public override void TakeAim(Transform target)
        {
            _projectileSocket.LookAt(target);
        }

        protected override void Shoot(ShootingData shootingData)
        {
            CreateProjectile(shootingData, _projectileSocket);
        }
    }
}