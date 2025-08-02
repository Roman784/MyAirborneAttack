using Zenject;

namespace UI
{
    public class GameplayPopUpProvider
    {
        private PopUp.Factory _defaultPopUp;

        [Inject]
        private void Construct(PopUp.Factory defaultPopUp)
        {
            _defaultPopUp = defaultPopUp;
        }

        public void OpenDefaultPopUp()
        {
            _defaultPopUp.Create().Open();
        }
    }
}