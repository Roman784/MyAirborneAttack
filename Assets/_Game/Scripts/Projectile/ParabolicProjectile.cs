using UnityEngine;

namespace Gameplay
{
    public class ParabolicProjectile : Projectile
    {
        private Vector3 _initialFlightVelocity;

        public ParabolicProjectile(ProjectileView view, ShootingData shootingData, float gravity, Vector3 flightDirection)
            : base(view, shootingData, gravity)
        {
            _initialFlightVelocity = flightDirection.normalized * shootingData.ProjectileFlightSpeed;

            if (_initialFlightVelocity != Vector3.zero)
                _view.Rotate(Quaternion.LookRotation(_initialFlightVelocity));
        }

        protected override void Move()
        {
            var x = _initialPosition.x + _initialFlightVelocity.x * _flightTime;
            var y = _initialPosition.y + _initialFlightVelocity.y * _flightTime + (_gravity * _flightTime * _flightTime) / 2f;
            var z = _initialPosition.z + _initialFlightVelocity.z * _flightTime;

            _view.Move(new Vector3(x, y, z));
        }

        // For editor.
        public static void DrawTrajectory(Transform socket, float initSpeed)
        {
            Gizmos.color = Color.red;

            var capacity = 50;
            var points = new Vector3[capacity];
            var initPos = socket.position;
            var initVel = -socket.transform.right * initSpeed;

            for (int i = 0; i < capacity; i++)
            {
                var t = i / 10f;
                var x = initPos.x + initVel.x * t;
                var y = initPos.y + initVel.y * t + (-9.81f * t * t) / 2f;
                var z = initPos.z + initVel.z * t;

                points[i] = new Vector3(x, y, z);
            }

            Gizmos.DrawLineList(points);
        }
    }
}