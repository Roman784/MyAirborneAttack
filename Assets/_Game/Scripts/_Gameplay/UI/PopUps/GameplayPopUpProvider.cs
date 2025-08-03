using Zenject;

namespace UI
{
    public class GameplayPopUpProvider
    {
        private GameOverPopUp.Factory _gameOverPopUp;
        private LevelPassedPopUp.Factory _levelPassedPopUp;

        [Inject]
        private void Construct(GameOverPopUp.Factory gameOverPopUp,
                               LevelPassedPopUp.Factory levelPassedPopUp)
        {
            _gameOverPopUp = gameOverPopUp;
            _levelPassedPopUp = levelPassedPopUp;
        }

        public void OpenGameOverPopUp()
        {
            _gameOverPopUp.Create().Open();
        }

        public void OpenLevelPassedPopUp()
        {
            _levelPassedPopUp.Create().Open();
        }
    }
}