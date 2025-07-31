using UnityEngine;

namespace Gameplay
{
    public class SingleShotShooting : Shooting
    {
        [SerializeField] private Transform _projectileSocket; // Point of projectile exit.

        public override void Shot(Projectile projectilePrefab, float projectileInitialFlightSpeed, float projectileDamage)
        {
            var newProjectile = Instantiate(projectilePrefab, _projectileSocket.position, Quaternion.identity); // TODO: From pool through factory.
            newProjectile.Init(-_projectileSocket.transform.right, projectileInitialFlightSpeed, projectileDamage);
        }

        // For editor.
        public override void DrawTrajectory(float initSpeed)
        {
            Gizmos.color = Color.red;

            var capacity = 50;
            var points = new Vector3[capacity];
            var initPos = _projectileSocket.position;
            var initVel = -_projectileSocket.transform.right * initSpeed;

            for (int i = 0; i < capacity; i++)
            {
                var t = i / 10f;
                var x = initPos.x + initVel.x * t;
                var y = initPos.y + initVel.y * t + (-9.8f * t * t) / 2f;
                var z = initPos.z + initVel.z * t;

                points[i] = new Vector3(x, y, z);
            }

            Gizmos.DrawLineList(points);
        }
    }
}