using UnityEngine;

namespace Gameplay
{
    public class Projectile : MonoBehaviour
    {
        private float _flightTime;
        private Vector3 _initialPosition;
        private Vector3 _initialFlightVelocity;
        private Vector3 _previousPosition;
        private float _damage;

        private float _gravity = -9.8f;

        public void Init(Vector3 flightDirection, float initialFlightSpeed, float damage)
        {
            _flightTime = 0f;
            _initialPosition = transform.position;
            _initialFlightVelocity = flightDirection.normalized * initialFlightSpeed;
            _damage = damage;

            if (_initialFlightVelocity != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(_initialFlightVelocity);

            Destroy(gameObject, 3); // TODO: Temp.
        }

        private void Update() // TODO: Temp.
        {
            _flightTime += Time.deltaTime;

            Move();
            Rotate();
        }

        private void Move()
        {
            var x = _initialPosition.x + _initialFlightVelocity.x * _flightTime;
            var y = _initialPosition.y + _initialFlightVelocity.y * _flightTime + (_gravity * _flightTime * _flightTime) / 2f;
            var z = _initialPosition.z + _initialFlightVelocity.z * _flightTime;

            _previousPosition = transform.position;
            transform.position = new Vector3(x, y, z);
        }

        private void Rotate()
        {
            var flightDirection = transform.position - _previousPosition;
            if (flightDirection == Vector3.zero) return;

            transform.rotation = Quaternion.LookRotation(flightDirection);
        }
    }
}