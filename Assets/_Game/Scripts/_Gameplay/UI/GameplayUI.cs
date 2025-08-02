using Gameplay;
using UnityEngine;
using Zenject;

namespace UI
{
    public class GameplayUI : SceneUI
    {
        [SerializeField] private WaveProgressUI _waveProgress;
        [SerializeField] private HealthBarUI _turretHealthBar;

        private GameplayPopUpProvider _popUpProvider;

        [Inject]
        private void Construct(GameplayPopUpProvider popUpProvider)
        {
            _popUpProvider = popUpProvider;
        }

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

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.O))
                _popUpProvider.OpenDefaultPopUp();
        }
    }
}