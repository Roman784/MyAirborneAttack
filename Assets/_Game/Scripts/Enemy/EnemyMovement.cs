using UnityEngine;

namespace Gameplay
{
    public class EnemyMovement
    {
        [field: SerializeField] public Vector3 Velocity;

        private readonly Transform _transform;
        private readonly EnemyPath _path;
        private readonly float _pathPassingRate;

        private float _pathPassingProgress = 0f;
        private Vector3 _previousPosition;

        public EnemyMovement(Transform transform, EnemyPath path, float pathPassingRate)
        {
            _transform = transform;
            _path = path;
            _pathPassingRate = pathPassingRate;

            _transform.position = _path.GetPosition(0);
        }

        public void MoveAlongPath(float deltaTime)
        {
            _pathPassingProgress += deltaTime * _pathPassingRate;

            if (_path.IsClosed)
                _pathPassingProgress = Mathf.Repeat(_pathPassingProgress, 1f);

            _previousPosition = _transform.position;

            _transform.position = _path.GetPosition(_pathPassingProgress);
            _transform.rotation = _path.GetRotation(_pathPassingProgress);

            Velocity = _transform.position - _previousPosition;
        }
    }
}