using UnityEngine;

namespace Gameplay
{
    public class FallingAnimation
    {
        private const float DRAG = 0.1f;
        private const float ANGULAR_DRAG = 0.5f;
        private const float VELOCITY_MULTIPLIER = 20f;
        private const float DEFAULT_VELOCITY_RANGE = 10f;
        private const float ANGULAR_VELOCITY_RANGE = 5f;
        private const float HORIZONTAL_ANGULAR_VELOCITY_MULTIPLIER = 5f;

        public FallingAnimation(GameObject target, Vector3 initialVelocity)
        {
            var rigidbody = target.AddComponent<Rigidbody>();

            rigidbody.drag = DRAG;
            rigidbody.angularDrag = ANGULAR_DRAG;

            rigidbody.velocity = GetVelocity(initialVelocity);
            rigidbody.angularVelocity = GetAngularVelocity();
        }

        private Vector3 GetVelocity(Vector3 initial)
        {
            if (initial == Vector3.zero)
                return new Vector3(
                    Random.Range(-DEFAULT_VELOCITY_RANGE, DEFAULT_VELOCITY_RANGE),
                    0f,
                    Random.Range(-DEFAULT_VELOCITY_RANGE, DEFAULT_VELOCITY_RANGE));

            return initial * VELOCITY_MULTIPLIER;
        }

        private Vector3 GetAngularVelocity()
        {
            return new Vector3(
                Random.Range(-ANGULAR_VELOCITY_RANGE, ANGULAR_VELOCITY_RANGE),
                Random.Range(-ANGULAR_VELOCITY_RANGE, ANGULAR_VELOCITY_RANGE) * HORIZONTAL_ANGULAR_VELOCITY_MULTIPLIER,
                Random.Range(-ANGULAR_VELOCITY_RANGE, ANGULAR_VELOCITY_RANGE)
            );
        }
    }
}