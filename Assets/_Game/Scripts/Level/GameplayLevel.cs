using Assets;
using Configs;
using GameRoot;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using WaveData = Configs.GameplayLevelConfig.WaveData;

namespace Gameplay
{
    public class GameplayLevel : MonoBehaviour
    {
        [SerializeField] private Transform _turretPoint;

        [SerializeField] private Enemy[] _enemies; // TODO: Temp.

        private Dictionary<string, EnemyPath> _enemyPathsMap = new();

        private IAssetsProvider _assetsProvider;
        private TurretFactory _turretFactory;

        [Inject]
        private void Construct(IAssetsProvider assetsProvider, TurretFactory turretFactory)
        {
            _assetsProvider = assetsProvider;
            _turretFactory = turretFactory;
        }

        public Turret CreateTurret(string nameId)
        {
            return _turretFactory.Create(nameId, _turretPoint.position);
        }

        public void LoadEnemyPaths(WaveData[] wavesData)
        {
            foreach (var wave in wavesData)
            {
                foreach (var enemySpawnData in wave.EnemySpawnSequenceData)
                {
                    var nameId = enemySpawnData.PathNameId;
                    if (_enemyPathsMap.ContainsKey(nameId)) continue;

                    var path = _assetsProvider.Load<EnemyPath>(AssetPaths.GAMEPLAY_ENEMY_PATHS_PREFABS + nameId);
                    _enemyPathsMap[nameId] = path;
                }
            }
        }
    }
}