using Assets;
using GameRoot;
using Zenject;

namespace Gameplay
{
    public class GameplayLevelFactory : Factory
    {
        public GameplayLevelFactory(DiContainer container, IAssetsProvider assetsProvider) 
            : base(container, assetsProvider)
        {
        }

        public GameplayLevel Create(string nameId)
        {
            var prefab = LoadPrefab<GameplayLevel>(AssetPaths.GAMEPLAY_LEVEL_PREFABS + nameId);
            return _container.InstantiatePrefabForComponent<GameplayLevel>(prefab);
        }
    }
}