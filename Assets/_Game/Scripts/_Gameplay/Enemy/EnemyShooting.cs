using UnityEngine;

namespace Gameplay
{
    public class EnemyShooting
    {
        private readonly Transform _transform;
        private readonly EnemyShootingData _shootingData;
        private readonly Turret _turret;
        private readonly Shooting _shooting;

        public EnemyShooting(Transform transform, Shooting shooting, 
                             EnemyShootingData shootingData, Turret turret)
        {
            _transform = transform;
            _shootingData = shootingData;
            _turret = turret;
            _shooting = shooting;

            _shooting.Init(shootingData);
        }

        public void TryShoot()
        {
            if (CanShoot())
            {
                _shooting.TakeAim(_turret.transform);
                _shooting.TryShoot();
            }
        }

        private bool CanShoot()
        {
            if (_turret == null) return false;

            var distanceToTurret = (_turret.transform.position - _transform.position).magnitude;
            return distanceToTurret < _shootingData.VisibilityRange;
        }
    }
}