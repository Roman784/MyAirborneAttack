using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public abstract class Shooting : MonoBehaviour
    {
        protected ProjectileFactory _projectileFactory;

        [Inject]
        private void Construct(ProjectileFactory projectileFactory)
        {
            _projectileFactory = projectileFactory;
        }

        public virtual void Shoot(ShootingData shootingData) { }
    }
}