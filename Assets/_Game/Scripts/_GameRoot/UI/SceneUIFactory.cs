using Zenject;

namespace UI
{
    public class SceneUIFactory
    {
        private DiContainer _container;
        private UIRoot _uiRoot;

        [Inject]
        public SceneUIFactory(DiContainer container, UIRoot uiRoot)
        {
            _container = container;
            _uiRoot = uiRoot;
        }

        public T Create<T>(T prefab) where T : SceneUI
        {
            var newUI = _container.InstantiatePrefabForComponent<T>(prefab);
            _uiRoot.AttachSceneUI(newUI);

            return newUI;
        }
    }
}