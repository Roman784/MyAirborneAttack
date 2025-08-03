using GameTick;
using R3;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayLevel : MonoBehaviour
    {
        [SerializeField] private WaveData[] _wavesData;

        private GameplayLevelView _view;
        private Dictionary<(string, EnemyPath), Stack<Enemy>> _enemiesMap = new();

        private WavesPassingService _wavesPassingService;

        private TurretFactory _turretFactory;
        private EnemyFactory _enemyFactory;
        private GameTickProvider _gameTickProvider;

        public Observable<Unit> LevelPassedSignal => _wavesPassingService?.AllWavesOverSignal;

        [Inject]
        private void Construct(TurretFactory turretFactory, EnemyFactory enemyFactory, 
                               GameTickProvider gameTickProvider,
                               GameplayLevelView view)
        {
            _view = view;

            _turretFactory = turretFactory;
            _enemyFactory = enemyFactory;
            _gameTickProvider = gameTickProvider;
        }

        public Turret CreateTurret(string nameId)
        {
            var turret = _turretFactory.Create(nameId, _view.TurretAnchor);
            turret.AttachCamera(_view.Camera);
            
            return turret;
        }

        // Creates all enemies for this level and disables their.
        public void PrepareEnemies(Turret turret)
        {
            foreach (var wave in _wavesData)
            {
                foreach (var spawnData in wave.SpawnSequenceData)
                {
                    var config = spawnData.EnemyConfig;
                    var path = spawnData.EnemyPath;
                    var key = spawnData.EnemyKey;

                    if (!_enemiesMap.TryGetValue(key, out var enemies))
                    {
                        enemies = new Stack<Enemy>();
                        _enemiesMap[key] = enemies;
                    }

                    var enemy = _enemyFactory.Create(config, path, turret);
                    enemy.Disable(true);

                    enemies.Push(enemy);
                }
            }
        }

        public Observable<(int, int)> PrepareWaves()
        {
            _wavesPassingService = new WavesPassingService(_wavesData, _enemiesMap);
            _gameTickProvider.AddTickable(_wavesPassingService);

            return _wavesPassingService.WaveStartSignal;
        }

        public void StartWaves()
        {
            _wavesPassingService.StartWaves();
        }

        private void OnDestroy()
        {
            if (_wavesPassingService != null)
                _gameTickProvider.RemoveTickable(_wavesPassingService);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            // Probable turret position.
            Gizmos.DrawSphere(Vector3.zero, 1);
        }
    }
}