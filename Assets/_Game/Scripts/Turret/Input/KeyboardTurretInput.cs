using UnityEngine;

namespace Gameplay
{
    public class KeyboardTurretInput : ITurretInput
    {
        // To normalize different inputs: touch, keyboard...
        private float _sensity = 1f;

        public Vector2 GetAxes()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            return new Vector2(horizontal, vertical) * _sensity;
        }

        public bool IsActive()
        {
            return Input.GetAxis("Horizontal") != 0 ||
                   Input.GetAxis("Vertical") != 0;
        }
    }
}