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
            var prefab = LoadPrefab<TurretView>(AssetPaths.GAMEPLAY_TURRET_PREFABS + nameId);
            
            var view = _container.InstantiatePrefabForComponent<TurretView>(prefab);
            
            view.transform.position = anchor.position;
            view.transform.rotation = anchor.rotation;
            view.transform.SetParent(anchor, false);

            var turret = _container.Instantiate<Turret>(new object[] { view });

            return turret;
        }
    }
}