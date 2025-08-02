using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;
using Zenject;
using GameTick;
using R3;
using UnityEditor;

namespace Gameplay
{
    public class ProjectileFactory
    {
        private readonly GameTickProvider _gameTickProvider;
        
        private Dictionary<string, ObjectPool<ProjectileView>> _viewsMap = new();

        [Inject]
        public ProjectileFactory(GameTickProvider gameTickProvider)
        {
            _gameTickProvider = gameTickProvider;
        }

        public ParabolicProjectile CreateParabolic(ShootingData shootingData, Vector3 position, Vector3 flightDirection)
        {
            var viewPrefab = shootingData.ProjectileViewPrefab;
            var view = CreateView(viewPrefab, position);
            var projectile = new ParabolicProjectile(view, shootingData, flightDirection);

            SetLifespan(projectile, view, _viewsMap[viewPrefab.NameId]);

            return projectile;
        }

        private ProjectileView CreateView(ProjectileView viewPrefab, Vector3 position)
        {
            if (!_viewsMap.TryGetValue(viewPrefab.NameId, out var viewsPool))
                CreateViewsPool(viewPrefab, out viewsPool);

            var view = viewsPool.Get();
            view.SetInitialPosition(position);

            return view;
        }

        private void CreateViewsPool(ProjectileView viewPrefab, out ObjectPool<ProjectileView> pool)
        {
            pool = new ObjectPool<ProjectileView>(
                createFunc: () => Object.Instantiate(viewPrefab),
                actionOnGet: (obj) => obj.Enable(),
                actionOnRelease: (obj) => obj.Disable(),
                defaultCapacity: 20);

            _viewsMap[viewPrefab.NameId] = pool;
        }

        private void SetLifespan(Projectile projectile, ProjectileView view, ObjectPool<ProjectileView> viewsPool)
        {
            _gameTickProvider.AddTickable(projectile);
            Observable.Merge(
                projectile.OnHitSignal.Select(_ => Unit.Default),
                projectile.LifeOverSignal)
            .Subscribe(_ =>
            {
                _gameTickProvider.RemoveTickable(projectile);
                viewsPool.Release(view);
            });
        }
    }
}