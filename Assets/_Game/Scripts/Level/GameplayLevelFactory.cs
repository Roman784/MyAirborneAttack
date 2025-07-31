using Assets;
using GameRoot;
using System.Collections.Generic;
using Zenject;

namespace GameplayLevel
{
    public class GameplayLevelFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetsProvider _assetsProvider;

        private Dictionary<string, GameplayLevel> _levelPrefabsMap = new();

        [Inject]
        public GameplayLevelFactory(DiContainer container, IAssetsProvider assetsProvider)
        {
            _container = container;
            _assetsProvider = assetsProvider;
        }

        public GameplayLevel Create(string nameId)
        {
            if (!_levelPrefabsMap.TryGetValue(nameId, out var prefab))
            {
                prefab = _assetsProvider.Load<GameplayLevel>(AssetPaths.GAMEPLAY_LEVEL_PREFABS + nameId);
                _levelPrefabsMap[nameId] = prefab;
            }

            return _container.InstantiatePrefabForComponent<GameplayLevel>(prefab);
        }
    }
}