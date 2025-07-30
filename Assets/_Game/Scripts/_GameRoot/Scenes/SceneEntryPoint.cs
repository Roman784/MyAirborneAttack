using System.Collections;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public abstract class SceneEntryPoint : MonoBehaviour
    {
        protected SceneProvider _sceneProvider;

        [Inject]
        private void Construct(SceneProvider sceneProvider)
        {
            _sceneProvider = sceneProvider;
        }

        public abstract IEnumerator Run<T>(T enterParams) where T : SceneEnterParams;
    }
}