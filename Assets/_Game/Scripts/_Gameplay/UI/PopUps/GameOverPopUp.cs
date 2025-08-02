namespace UI
{
    public class GameOverPopUp : PopUp
    {
        public void RestartLevel()
        {
            _sceneProvider.TryRestartScene();
        }

        public class Factory : PopUpFactory<GameOverPopUp>
        {
        }
    }
}