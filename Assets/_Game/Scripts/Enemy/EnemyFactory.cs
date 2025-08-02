using Assets;
using Configs;
using GameRoot;
using System;
using Zenject;
using R3;

namespace Gameplay
{
    public class EnemyFactory : Factory
    {
        public EnemyFactory(DiContainer container, IAssetsProvider assetsProvider) 
            : base(container, assetsProvider)
        {
        }

        public Enemy Create(EnemyConfig config, EnemyPath path, Turret turret)
        {
            var nameId = config.NameId;
            var prefab = LoadPrefab<Enemy>(AssetPaths.GAMEPLAY_ENEMY_PREFABS + nameId);

            var enemy = _container.InstantiatePrefabForComponent<Enemy>(prefab)
                .Init(config, path, turret);

            return enemy;
        }
    }
}