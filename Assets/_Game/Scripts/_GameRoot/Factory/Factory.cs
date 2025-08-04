using Assets;
using System.Collections.Generic;
using Zenject;
using Object = UnityEngine.Object;

namespace GameRoot
{
    public abstract class Factory
    {
        protected readonly DiContainer _container;
        protected readonly IAssetsProvider _assetsProvider;

        private Dictionary<string, Object> _prefabsMap = new();

        [Inject]
        public Factory(DiContainer container, IAssetsProvider assetsProvider)
        {
            _container = container;
            _assetsProvider = assetsProvider;
        }

        public T LoadPrefab<T>(string path) where T : Object
        {
            if (!_prefabsMap.TryGetValue(path, out var prefab))
            {
                prefab = _assetsProvider.Load<T>(path);
                _prefabsMap[path] = prefab;
            }

            return (T)prefab;
        }
    }
}