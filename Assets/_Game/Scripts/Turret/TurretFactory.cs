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

        public Turret Create(string nameId, Vector3 position, Quaternion rotation)
        {
            var prefab = LoadPrefab<TurretView>(AssetPaths.GAMEPLAY_TURRET_PREFABS + nameId);
            
            var view = _container.InstantiatePrefabForComponent<TurretView>(prefab);
            view.transform.position = position;
            view.transform.rotation = rotation;

            var turret = _container.Instantiate<Turret>(new object[] { view });

            _disposables.Add(turret);
            turret.OnDeathSignal.Subscribe(_ => _disposables.Remove(turret));

            return turret;
        }
    }
}