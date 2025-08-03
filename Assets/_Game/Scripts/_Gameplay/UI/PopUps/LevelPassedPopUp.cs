namespace UI
{
    public class LevelPassedPopUp : PopUp
    {
        public void RestartLevel()
        {
            _sceneProvider.TryRestartScene();
        }

        public class Factory : PopUpFactory<LevelPassedPopUp>
        {
        }
    }
}