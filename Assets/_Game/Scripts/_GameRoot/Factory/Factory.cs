using Assets;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GameRoot
{
    public abstract class Factory : IDisposable
    {
        protected readonly DiContainer _container;
        protected readonly IAssetsProvider _assetsProvider;

        protected List<IDisposable> _disposables = new();

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

        public void Dispose()
        {
            _disposables.ForEach(t => t.Dispose());
        }
    }
}