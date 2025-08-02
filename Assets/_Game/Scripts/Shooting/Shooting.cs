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

        protected abstract void Shoot(ShootingData shootingData);
    }
}