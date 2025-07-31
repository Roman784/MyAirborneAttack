using UnityEngine;

namespace Assets
{
    public interface IAssetsProvider
    {
        public T Load<T>(string path) where T : Object;
    }
}