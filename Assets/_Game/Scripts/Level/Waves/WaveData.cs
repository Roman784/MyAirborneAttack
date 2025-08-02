using System;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public class WaveData
    {
        [field: SerializeField] public WaveSpawnData[] SpawnSequenceData { get; private set; }
    }
}