using Assets;
using GameRoot;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class TurretFactory : Factory
    {
        public TurretFactory(DiContainer container, IAssetsProvider assetsProvider) 
            : base(container, assetsProvider)
        {
        }

        public Turret Create(string nameId, Vector3 position)
        {
            var prefab = LoadPrefab<TurretView>(AssetPaths.GAMEPLAY_TURRET_PREFABS + nameId);
            
            var view = _container.InstantiatePrefabForComponent<TurretView>(prefab);
            view.transform.position = position;

            var turret = _container.Instantiate<Turret>(new object[] { view });

            _disposables.Add(turret);

            return turret;
        }
    }
}