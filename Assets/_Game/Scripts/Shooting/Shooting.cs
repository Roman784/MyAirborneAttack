using UnityEngine;

namespace Gameplay
{
    public abstract class Shooting : MonoBehaviour
    {
        public abstract void Shot(Projectile projectilePrefab, float projectileInitialFlightSpeed, float projectileDamage);
        public virtual void DrawTrajectory(float initSpeed) { }
    }
}