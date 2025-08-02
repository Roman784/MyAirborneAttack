using System.Collections;
using UnityEngine;
using R3;

namespace UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private Transform _sceneUIContainer;
        [SerializeField] private LoadingScreen _loadingScreen;

        public IEnumerator ShowLoadingScreen()
        {
            yield return SetLoadingScreen(true);
        }

        public IEnumerator HideLoadingScreen()
        {
            yield return SetLoadingScreen(false);
        }

        public void AttachSceneUI(SceneUI sceneUI)
        {
            sceneUI.transform.SetParent(_sceneUIContainer, false);
        }

        public void ClearAllContainers()
        {
            ClearContainer(_sceneUIContainer);
        }

        private void ClearContainer(Transform container)
        {
            var childCount = container.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(container.GetChild(i).gameObject);
            }
        }

        private IEnumerator SetLoadingScreen(bool value)
        {
            bool isCompleted = false;

            (value ? _loadingScreen.Show() : _loadingScreen.Hide())
                .Subscribe(_ => isCompleted = true);

            yield return new WaitUntil(() => isCompleted);
        }
    }
}