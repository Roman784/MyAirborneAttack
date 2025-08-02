using UnityEngine;

namespace UI
{
    public class GameplayUI : SceneUI
    {
        [SerializeField] private WaveProgressUI _waveProgress;

        public void ShowWaveProgress(int current, int total)
        {
            _waveProgress.Show(current, total);
        }
    }
}