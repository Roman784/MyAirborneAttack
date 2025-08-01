using R3;
using UnityEngine;

namespace Gameplay
{
    public class TurretRotationController : MonoBehaviour
    {
        [SerializeField] private Transform _rotor;
        [SerializeField] private Transform _barrel;

        [Space]

        [SerializeField] private Vector2 _speed = new (50f, 50f);
        [SerializeField] private Vector2 _maxSpeed = new (75f, 75f);
        [SerializeField] private Vector2 _barrelClampAngles = new (-60f, 30f);
        [SerializeField] private float _initialBarrelAngle = -15f;

        private Vector2 _currentAngles;

        public float InitialBarrelAngle => _initialBarrelAngle;
        private Subject<Vector2> _anglesChangedSignalSubj = new();
        public Observable<Vector2> AnglesChangedSignal => _anglesChangedSignalSubj;

        private void Start()
        {
            _currentAngles.y = _initialBarrelAngle;
            RotateBarrel(_initialBarrelAngle);
        }

        public void Rotate(Vector2 inputAxes, float deltaTime)
        {
            var velocity = ClampVelocity(inputAxes * _speed);

            // Horizontal rotation.
            _currentAngles.x += velocity.x * deltaTime;
            RotateRotor(_currentAngles.x);

            // Vertical rotation.
            _currentAngles.y -= velocity.y * deltaTime;
            RotateBarrel(_currentAngles.y);

            _anglesChangedSignalSubj.OnNext(_currentAngles);
        }

        private void RotateRotor(float angle)
        {
            _rotor.localRotation = Quaternion.Euler(0f, angle, 0f);
        }

        private void RotateBarrel(float angle)
        {
            angle = Mathf.Clamp(angle, _barrelClampAngles.x, _barrelClampAngles.y);
            _barrel.localRotation = Quaternion.Euler(0f, 0f, angle);
        }

        private Vector2 ClampVelocity(Vector2 velocity)
        {
            velocity.x = Mathf.Clamp(velocity.x, -_maxSpeed.x, _maxSpeed.x);
            velocity.y = Mathf.Clamp(velocity.y, -_maxSpeed.y, _maxSpeed.y);
            return velocity;
        }
    }
}