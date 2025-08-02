using Zenject;

namespace UI
{
    public class GameplayPopUpProvider
    {
        private GameOverPopUp.Factory _gameOverPopUp;

        [Inject]
        private void Construct(GameOverPopUp.Factory gameOverPopUp)
        {
            _gameOverPopUp = gameOverPopUp;
        }

        public void OpenGameOverPopUp()
        {
            _gameOverPopUp.Create().Open();
        }
    }
}