using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets
{
    public class ResourcesAssetsProvider : IAssetsProvider
    {
        public T Load<T>(string path) where T : Object
        {
            T asset = Resources.Load<T>(path);

            if (asset == null)
                throw new NullReferenceException($"Failed to load asset '{path}'!");

            return asset;
        }
    }
}