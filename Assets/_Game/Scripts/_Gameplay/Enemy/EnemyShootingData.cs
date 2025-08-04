using System;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public class EnemyShootingData : ShootingData
    {
        [field: SerializeField] public float VisibilityRange { get; private set; }
    }
}