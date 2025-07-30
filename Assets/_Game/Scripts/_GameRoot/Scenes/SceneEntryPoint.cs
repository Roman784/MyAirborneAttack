using Configs;
using System.Collections;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public abstract class SceneEntryPoint : MonoBehaviour
    {
        protected SceneProvider _sceneProvider;
        protected IConfigProvider _configProvider;

        [Inject]
        private void Construct(SceneProvider sceneProvider, IConfigProvider configProvider)
        {
            _sceneProvider = sceneProvider;
            _configProvider = configProvider;
        }

        public abstract IEnumerator Run<T>(T enterParams) where T : SceneEnterParams;
    }
}