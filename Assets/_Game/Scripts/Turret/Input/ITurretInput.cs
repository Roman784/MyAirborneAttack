using UnityEngine;

namespace Gameplay
{
    public interface ITurretInput
    {
        public Vector2 GetAxes();
        public bool IsActive();
    }
}