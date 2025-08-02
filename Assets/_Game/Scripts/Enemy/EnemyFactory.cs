using Assets;
using Configs;
using GameRoot;
using System;
using Zenject;
using R3;

namespace Gameplay
{
    public class EnemyFactory : Factory, IDisposable
    {
        public EnemyFactory(DiContainer container, IAssetsProvider assetsProvider) 
            : base(container, assetsProvider)
        {
        }

        public Enemy Create(EnemyConfig config, EnemyPath path)
        {
            var nameId = config.NameId;
            var prefab = LoadPrefab<EnemyView>(AssetPaths.GAMEPLAY_ENEMY_PREFABS + nameId);

            var view = _container.InstantiatePrefabForComponent<EnemyView>(prefab);
            var enemy = _container.Instantiate<Enemy>(new object[] { view, config, path });

            _disposables.Add(enemy);
            enemy.OnDeathSignal.Subscribe(_ => _disposables.Remove(enemy));

            return enemy;
        }
    }
}