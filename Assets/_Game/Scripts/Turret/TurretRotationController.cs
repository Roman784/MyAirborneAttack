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

        private Vector2 _currentRotation;

        public void Rotate(Vector2 inputAxes, float deltaTime)
        {
            var velocity = ClampVelocity(inputAxes * _speed);

            // Horizontal rotation.
            _currentRotation.x += velocity.x * deltaTime;
            _rotor.localRotation = Quaternion.Euler(0f, _currentRotation.x, 0f);

            // Vertical rotation.
            _currentRotation.y -= velocity.y * deltaTime;
            _currentRotation.y = Mathf.Clamp(_currentRotation.y, _barrelClampAngles.x, _barrelClampAngles.y);
            _barrel.localRotation = Quaternion.Euler(0f, 0f, _currentRotation.y);
        }

        private Vector2 ClampVelocity(Vector2 velocity)
        {
            velocity.x = Mathf.Clamp(velocity.x, -_maxSpeed.x, _maxSpeed.x);
            velocity.y = Mathf.Clamp(velocity.y, -_maxSpeed.y, _maxSpeed.y);
            return velocity;
        }
    }
}