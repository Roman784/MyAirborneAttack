using Assets;
using GameRoot;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class TurretFactory : Factory, IDisposable
    {
        private List<IDisposable> _spawnedTurrets = new();

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
            _spawnedTurrets.Add(turret);

            return turret;
        }

        public void Dispose()
        {
            _spawnedTurrets.ForEach(t => t.Dispose());
        }
    }
}