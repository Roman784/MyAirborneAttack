using UnityEngine;

namespace Gameplay
{
    public class TurretShooting
    {
        private readonly ShootingData _shootingData;
        private readonly Shooting _shooting;

        public TurretShooting(ShootingData shootingData, Shooting shooting)
        { 
            _shootingData = shootingData;
            _shooting = shooting;

            _shooting.Init(_shootingData);
        }

        public void TryShoot()
        {
            _shooting.TryShoot();
        }
    }
}