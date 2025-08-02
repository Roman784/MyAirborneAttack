using Configs;
using System;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public class WaveSpawnData
    {
        [field: SerializeField] public float TimeSinceWaveStart { get; private set; }
        [field: SerializeField] public EnemyConfig EnemyConfig { get; private set; }
        [field: SerializeField] public EnemyPath EnemyPath { get; private set; }
    }
}