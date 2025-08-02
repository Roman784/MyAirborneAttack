using GameTick;
using System.Collections.Generic;
using System.Linq;
using R3;

namespace Gameplay
{
    public class WavesPassingService : ITickable
    {
        private readonly WaveData[] _wavesData;
        private readonly Dictionary<(string, EnemyPath), Stack<Enemy>> _enemiesMap;

        private int _waveIdx;
        private float _waveTime;
        private bool[] _completedSpawns;
        private bool _areAllWavesOver;

        private List<Enemy> _spawnedEnemies = new();

        private WaveData CurrentWave => _wavesData[_waveIdx];

        public WavesPassingService(IEnumerable<WaveData> wavesData, 
                                   IReadOnlyDictionary<(string, EnemyPath), Stack<Enemy>> enemiesMap)
        {
            _wavesData = wavesData.ToArray();
            _enemiesMap = new Dictionary<(string, EnemyPath), Stack<Enemy>>(enemiesMap);

            _waveIdx = -1;
            StartNextWave();
        }

        public void Tick(float deltaTime)
        {
            if (_areAllWavesOver) return;

            _waveTime += deltaTime;

            var spawnSequenceData = CurrentWave.SpawnSequenceData;
            for (int i = 0; i < spawnSequenceData.Length; i++)
            {
                var spawnData = spawnSequenceData[i];
                var startTime = spawnData.TimeSinceWaveStart;

                if (_completedSpawns[i] || _waveTime < startTime) continue;

                Spawn(spawnData);
                _completedSpawns[i] = true;
            }

            if (IsWavePassed())
                StartNextWave();
        }

        private void StartNextWave()
        {
            _waveTime = 0f;

            if (_waveIdx < _wavesData.Length - 1)
            {
                _waveIdx += 1;
                _completedSpawns = new bool[CurrentWave.SpawnSequenceData.Length];
                _spawnedEnemies = new List<Enemy>();
            }
            else
            {
                _areAllWavesOver = true;
            }
        }


        private void Spawn(WaveSpawnData spawnData)
        {
            var enemyKey = spawnData.EnemyKey;
            var enemy = _enemiesMap[enemyKey].Pop();

            _spawnedEnemies.Add(enemy);
            enemy.OnDeathSignal.Subscribe(_ => _spawnedEnemies.Remove(enemy));

            enemy.Enable();
        }

        private bool IsWavePassed()
        {
            if (_spawnedEnemies.Count != 0) return false;

            foreach (var spawnStatus in _completedSpawns)
                if (!spawnStatus) return false;

            return true;
        }
    }
}