using Assets;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public abstract class Factory
    {
        protected readonly DiContainer _container;
        protected readonly IAssetsProvider _assetsProvider;

        [Inject]
        public Factory(DiContainer container, IAssetsProvider assetsProvider)
        {
            _container = container;
            _assetsProvider = assetsProvider;
        }

        public T LoadPrefab<T>(string path) where T : Object
        {
            return _assetsProvider.Load<T>(path);
        }
    }
}