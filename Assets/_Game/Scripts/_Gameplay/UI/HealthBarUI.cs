using Gameplay;
using UnityEngine;
using UnityEngine.UI;
using R3;
using DG.Tweening;
using Utils;

namespace UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Image _view;
        [SerializeField] private TweenData _changing;

        private Tween _changingTween;

        public void Init(Health health)
        {
            health.DamageSignal.Subscribe(_ => CnangeView(health.Ratio));
        }

        private void CnangeView(float fill)
        {
            _changingTween?.Kill();
            _changingTween = _view.DOFillAmount(fill, _changing.Duration)
                .SetEase(_changing.Ease);
        }

        private void OnDestroy()
        {
            _changingTween?.Kill();
        }
    }
}