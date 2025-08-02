using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PopUpsRoot : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;

        private Stack<PopUp> _popUps = new();

        public void AttachPopUp(PopUp popUp)
        {
            if (_popUps.Count > 0)
                _popUps.Peek().Close(false);

            _popUps.Push(popUp);
            popUp.transform.SetParent(_container, false);
        }

        public void CloseCurrentPopUp()
        {
            _popUps.Pop();

            if (_popUps.Count > 0)
                _popUps.Peek().Open();
        }

        public void DestroyAllPopUps()
        {
            foreach (var popUp in _popUps)
            {
                Destroy(popUp.gameObject);
            }

            _popUps.Clear();
        }
    }
}