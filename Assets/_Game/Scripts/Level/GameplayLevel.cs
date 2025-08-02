using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayLevel : MonoBehaviour
    {
        [SerializeField] private WaveData[] _wavesData;

        private GameplayLevelView _view;

        private Dictionary<string, Stack<Enemy>> _enemiesMap = new();

        private TurretFactory _turretFactory;
        private EnemyFactory _enemyFactory;

        [Inject]
        private void Construct(TurretFactory turretFactory, EnemyFactory enemyFactory,
                               GameplayLevelView view)
        {
            _view = view;

            _turretFactory = turretFactory;
            _enemyFactory = enemyFactory;
        }

        public Turret CreateTurret(string nameId)
        {
            var turret = _turretFactory.Create(nameId, _view.TurretAnchor);
            turret.AttachCamera(_view.Camera);
            
            return turret;
        }

        // Creates all enemies for this level and disables their.
        public void PrepareEnemies()
        {
            foreach (var wave in _wavesData)
            {
                foreach (var spawnData in wave.SpawnSequenceData)
                {
                    var config = spawnData.EnemyConfig;
                    var nameId = config.NameId;
                    var path = spawnData.EnemyPath;

                    if (!_enemiesMap.TryGetValue(nameId, out var enemies))
                        enemies = new Stack<Enemy>();

                    var enemy = _enemyFactory.Create(config, path);
                    enemy.Disable();

                    enemies.Push(enemy);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            // Probable turret position.
            Gizmos.DrawSphere(Vector3.zero, 1);
        }
    }
}