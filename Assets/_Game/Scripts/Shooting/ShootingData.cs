using System;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public class ShootingData
    {
        [field: SerializeField] public ProjectileView ProjectileViewPrefab { get; private set; }
        [field: SerializeField] public ProjectileType ProjectileType { get; private set; }
        [field: SerializeField] public float Rate { get; private set; }
        [field: SerializeField] public float ProjectileFlightSpeed { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public LayerMask TargetLayer { get; private set; }
    }
}