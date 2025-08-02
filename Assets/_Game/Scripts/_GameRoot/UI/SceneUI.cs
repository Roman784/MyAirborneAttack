using GameRoot;
using UnityEngine;
using Zenject;

namespace UI
{
    public class SceneUI : MonoBehaviour
    {
        protected SceneProvider _sceneProvider;

        [Inject]
        private void Construct(SceneProvider sceneProvider)
        {
            _sceneProvider = sceneProvider;
        }
    }
}