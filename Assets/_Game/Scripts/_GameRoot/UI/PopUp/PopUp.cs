using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace UI
{
    public class PopUp : MonoBehaviour
    {
        [SerializeField] private RectTransform _view;
        [SerializeField] private CanvasGroup _fade;

        [Space]

        [SerializeField] private TweenData _opening;
        [SerializeField] private TweenData _closing;
        [SerializeField] private TweenData _fading;

        private PopUpsRoot _root;

        private Tween _viewChangingTween;
        private Tween _fadingTween;

        [Inject]
        private void Construct(UIRoot uiRoot)
        {
            _root = uiRoot.PopUpsRoot;
        }

        public virtual void Open()
        {
            _view.localScale = Vector3.zero;
            _viewChangingTween = _view.DOScale(1f, _opening.Duration)
                .SetEase(_opening.Ease);

            _fade.alpha = 0f;
            _fadingTween = _fade.DOFade(1f, _fading.Duration)
                .SetEase(_fading.Ease);
        }

        public virtual void Close(bool destroy = false)
        {
            _viewChangingTween = _view.DOScale(0f, _closing.Duration)
                .SetEase(_closing.Ease)
                .OnComplete(() =>
                {
                    if (destroy) Destroy();
                });

            _fadingTween = _fade.DOFade(0f, _fading.Duration)
                .SetEase(_fading.Ease);
        }

        protected void Destroy()
        {
            _root.CloseCurrentPopUp();
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _viewChangingTween?.Kill();
            _fadingTween?.Kill();
        }

        public class Factory : PopUpFactory<PopUp>
        {
        }
    }
}