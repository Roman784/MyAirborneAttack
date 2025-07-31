using UnityEngine;

namespace Gameplay
{
    public class TouchTurretInput : ITurretInput
    {
        // To normalize different inputs: touch, keyboard...
        private float _sensity = 10f;

        private Vector2 _initialTouchPosition;
        private Vector2 _touchDelta;

        public Vector2 GetAxes()
        {
            if (!IsActive()) return Vector2.zero;

            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    HandleTouchBegan(touch);
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    HandleTouchMoved(touch);
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    HandleTouchEnded();
                    break;
            }

            NormalizeTouchDelta();

            return _touchDelta;
        }

        public bool IsActive()
        {
            return Input.touchCount > 0;
        }

        private void HandleTouchBegan(Touch touch)
        {
            _initialTouchPosition = touch.position;
            _touchDelta = Vector2.zero;
        }

        private void HandleTouchMoved(Touch touch)
        {
            _touchDelta = touch.position - _initialTouchPosition;
        }

        private void HandleTouchEnded()
        {
            _touchDelta = Vector2.zero;
        }

        // Normalizes delta by screen size and sensitivity.
        private void NormalizeTouchDelta()
        {
            _touchDelta.x /= Screen.width;
            _touchDelta.y /= Screen.height;

            _touchDelta *= _sensity;
        }
    }
}