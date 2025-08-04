using Assets;
using GameRoot;
using UnityEngine;
using Zenject;
using R3;

namespace Gameplay
{
    public class TurretFactory : Factory
    {
        public TurretFactory(DiContainer container, IAssetsProvider assetsProvider) 
            : base(container, assetsProvider)
        {
        }

        public Turret Create(string nameId, Transform anchor)
        {
            var prefab = LoadPrefab<Turret>(AssetPaths.GAMEPLAY_TURRET_PREFABS + nameId);
            var turret = _container.InstantiatePrefabForComponent<Turret>(prefab)
                .Init(anchor);

            return turret;
        }
    }
}