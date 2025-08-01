using Assets;
using GameRoot;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using WaveData = Configs.GameplayLevelConfig.WaveData;

namespace Gameplay
{
    public class GameplayLevel : MonoBehaviour
    {
        [SerializeField] private TrackingCamera _camera;
        [SerializeField] private Transform _turretAnchor;

        private Dictionary<(string, float), EnemyPath> _enemyPathsMap = new();
        private Dictionary<string, Stack<Enemy>> _enemiesMap = new();

        private IAssetsProvider _assetsProvider;
        private TurretFactory _turretFactory;
        private EnemyFactory _enemyFactory;

        [Inject]
        private void Construct(IAssetsProvider assetsProvider, 
                               TurretFactory turretFactory, EnemyFactory enemyFactory)
        {
            _assetsProvider = assetsProvider;
            _turretFactory = turretFactory;
            _enemyFactory = enemyFactory;
        }

        public Turret CreateTurret(string nameId)
        {
            var turret = _turretFactory.Create(nameId, _turretAnchor.position, _turretAnchor.rotation);
            turret.AttachCamera(_camera);
            
            return turret;
        }

        public void CreateEnemyPaths(WaveData[] wavesData)
        {
            foreach (var wave in wavesData)
            {
                foreach (var enemySpawnData in wave.EnemySpawnSequenceData)
                {
                    var nameId = enemySpawnData.PathNameId;
                    var height = enemySpawnData.PathHeight;
                    var key = enemySpawnData.PathKey;

                    if (_enemyPathsMap.ContainsKey(key)) continue;

                    var pathPrefab = _assetsProvider.Load<EnemyPath>(AssetPaths.GAMEPLAY_ENEMY_PATHS_PREFABS + nameId);
                    var path = Instantiate(pathPrefab, new Vector3(0f, height, 0f), Quaternion.identity);

                    _enemyPathsMap[key] = path;
                }
            }
        }

        public void CreateEnemies(WaveData[] wavesData)
        {
            foreach (var wave in wavesData)
            {
                foreach (var enemySpawnData in wave.EnemySpawnSequenceData)
                {
                    var enemyConfig = enemySpawnData.EnemyConfig;
                    var nameId = enemyConfig.NameId;
                    var path = _enemyPathsMap[enemySpawnData.PathKey];

                    if (!_enemiesMap.TryGetValue(nameId, out var enemies))
                        enemies = new Stack<Enemy>();

                    var enemy = _enemyFactory.Create(enemyConfig, path);
                    enemies.Push(enemy);
                }
            }
        }
    }
}