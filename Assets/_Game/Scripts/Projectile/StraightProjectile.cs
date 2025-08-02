using UnityEngine;

namespace Gameplay
{
    public class StraightProjectile : ParabolicProjectile
    {
        public StraightProjectile(ProjectileView view, ShootingData shootingData, Vector3 flightDirection) 
            : base(view, shootingData, 0f, flightDirection)
        {
        }

        // For editor.
        public new static void DrawTrajectory(Transform socket, float initSpeed)
        {
            Gizmos.color = Color.red;

            var capacity = 50;
            var points = new Vector3[capacity];
            var initPos = socket.position;
            var initVel = -socket.transform.right * initSpeed;

            for (int i = 0; i < capacity; i++)
            {
                var t = i / 10f;
                points[i] = initPos + initVel * t;
            }

            Gizmos.DrawLineList(points);
        }
    }
}