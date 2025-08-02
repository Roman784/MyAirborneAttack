using DG.Tweening;
using R3;
using UnityEngine;
using Utils;

namespace UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _view;

        [Space]

        [SerializeField] private TweenData _showing;
        [SerializeField] private TweenData _hiding;

        private void Awake()
        {
            _view.gameObject.SetActive(true);
            _view.alpha = 1f;
        }

        public Observable<Unit> Show()
        {
            var onCompleted = new Subject<Unit>();

            _view.gameObject.SetActive(true);
            _view.DOFade(1f, _showing.Duration)
                .SetEase(_showing.Ease)
                .OnComplete(() => onCompleted.OnNext(Unit.Default));

            return onCompleted;
        }

        public Observable<Unit> Hide()
        {
            var onCompleted = new Subject<Unit>();

            _view.DOFade(0f, _hiding.Duration)
                .SetEase(_hiding.Ease)
                .OnComplete(() =>
                {
                    onCompleted.OnNext(Unit.Default);
                    _view.gameObject.SetActive(false);
                });

            return onCompleted;
        }
    }
}