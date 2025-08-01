using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GameplayLevelConfig", 
                     menuName = "Game Configs/Gameplay level/New Gameplay Level Config", order = 1)]
    public class GameplayLevelConfig : ScriptableObject
    {
        [field: SerializeField] public string NameId { get; private set; }
        [field: SerializeField] public int Number { get; private set; }

        [field: Space]

        [field:SerializeField] public WaveData[] WavesData { get; private set; }

        [Serializable]
        public class WaveData
        {
            [field: SerializeField] public EnemySpawnData[] EnemySpawnSequenceData { get; private set; }
        }

        [Serializable]
        public class EnemySpawnData
        {
            [field: SerializeField] public float TimeSinceWaveStart { get; private set; }
            [field: SerializeField] public string EnemyNameId { get; private set; }
            [field: SerializeField] public string PathNameId { get; private set; }
            [field: SerializeField] public float PathHeight { get; private set; }
        }
    }
}