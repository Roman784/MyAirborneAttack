using DG.Tweening;
using TMPro;
using UnityEngine;
using Utils;

namespace UI
{
    public class WaveProgressUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _view;
        [SerializeField] private RectTransform _transform;

        [Space]

        [SerializeField] private float _yPos;

        [Space]


        [SerializeField] private TweenData _showing;
        [SerializeField] private TweenData _hiding;

        [Space]

        [SerializeField] private float _delayBeforeShowing;
        [SerializeField] private float _delayBeforeHiding;

        private Sequence _showingSequence;

        public void Show(int currentWave, int totalWaves)
        {
            _view.text = $"NEW WAVE\n{currentWave} of {totalWaves}";

            _showingSequence?.Kill();
            _showingSequence = DOTween.Sequence();

            _showingSequence.AppendInterval(_delayBeforeShowing);
            _showingSequence.Append(_transform.DOAnchorPosY(_yPos, _showing.Duration).SetEase(_showing.Ease));
            _showingSequence.AppendInterval(_delayBeforeHiding);
            _showingSequence.Append(_transform.DOAnchorPosY(0f, _hiding.Duration).SetEase(_hiding.Ease));
            _showingSequence.Play();
        }

        private void OnDestroy()
        {
            _showingSequence?.Kill();
        }
    }
}