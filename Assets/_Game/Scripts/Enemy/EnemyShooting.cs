using System;
using UnityEngine;

namespace Gameplay
{
    public class EnemyShooting : MonoBehaviour
    {
        private EnemyShootingData _shootingData;
        private Turret _turret;
        private Shooting _shooting;

        public EnemyShooting Init(EnemyShootingData shootingData, Turret turret)
        {
            _shootingData = shootingData;
            _turret = turret;
            _shooting = GetComponent<Shooting>();

            if (_shooting == null)
                throw new NullReferenceException("Shooting component not found!");

            _shooting.Init(shootingData);

            return this;
        }

        public void Shoot()
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

            var distanceToTurret = (_turret.Position - transform.position).magnitude;

            return distanceToTurret < _shootingData.VisibilityRange;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _shootingData.VisibilityRange);
        }
    }
}