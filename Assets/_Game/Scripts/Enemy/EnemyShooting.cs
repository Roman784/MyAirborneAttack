using UnityEngine;

namespace Gameplay
{
    public class EnemyShooting
    {
        private Transform _transform;
        private EnemyShootingData _shootingData;
        private Turret _turret;
        private Shooting _shooting;

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
                _shooting.TakeAim(_turret.Transform);
                _shooting.TryShoot();
            }
        }

        private bool CanShoot()
        {
            if (_turret == null) return false;

            var distanceToTurret = (_turret.Position - _transform.position).magnitude;
            return distanceToTurret < _shootingData.VisibilityRange;
        }
    }
}