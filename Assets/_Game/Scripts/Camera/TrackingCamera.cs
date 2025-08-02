using R3;
using UnityEngine;

namespace Gameplay
{
    public class TrackingCamera : MonoBehaviour
    {
        [SerializeField] private float _horizontalRotationOffset;

        public void Attach(Transform locationTarget, Observable<Vector2> rotationSignalSubj, Vector2 initialRotation)
        {
            Move(locationTarget);
            Rotate(initialRotation);

            rotationSignalSubj.Subscribe(angles => Track(locationTarget, angles));
        }

        private void Track(Transform locationTarget, Vector2 angles)
        {
            Move(locationTarget);
            Rotate(angles);
        }

        private void Move(Transform locationTarget)
        {
            transform.position = locationTarget.position;
        }

        private void Rotate(Vector2 angles)
        {
            angles.y += _horizontalRotationOffset;
            transform.rotation = Quaternion.Euler(angles.y, angles.x, 0f);
        }
    }
}