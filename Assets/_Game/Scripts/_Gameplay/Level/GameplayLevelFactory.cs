using Assets;
using Configs;
using GameRoot;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayLevelFactory : Factory
    {
        public GameplayLevelFactory(DiContainer container, IAssetsProvider assetsProvider) 
            : base(container, assetsProvider)
        {
        }

        public GameplayLevel Create(GameplayLevelConfig config)
        {
            var levelNameId = config.NameId;
            var viewNameId = config.ViewNameId;

            var levelPrefab = LoadPrefab<GameplayLevel>(AssetPaths.GAMEPLAY_LEVEL_PREFABS + levelNameId);
            var viewPrefab = LoadPrefab<GameplayLevelView>(AssetPaths.GAMEPLAY_LEVEL_VIEW_PREFABS + viewNameId);

            var view = Object.Instantiate(viewPrefab);
            var level = _container.InstantiatePrefabForComponent<GameplayLevel>(levelPrefab, new object[] { view });

            view.transform.SetParent(level.transform, false);

            return level;
        }
    }
}