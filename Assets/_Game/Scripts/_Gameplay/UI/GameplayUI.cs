using Gameplay;
using UnityEngine;

namespace UI
{
    public class GameplayUI : SceneUI
    {
        [SerializeField] private WaveProgressUI _waveProgress;
        [SerializeField] private HealthBarUI _turretHealthBar;

        public void ShowWaveProgress(int current, int total)
        {
            _waveProgress.Show(current, total);
        }

        public void InitTurrentHealthBar(Turret turret)
        {
            _turretHealthBar.Init(turret.Health);
        }

        public void RestartLevel()
        {
            _sceneProvider.TryRestartScene();
        }
    }
}