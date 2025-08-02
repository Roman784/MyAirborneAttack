using System;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class TurretShootingContoller : MonoBehaviour
    {
        [SerializeField] private ShootingData _shootingData;

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
    }
}