using UnityEngine;
using UnityEngine.Splines;

namespace Gameplay
{
    [RequireComponent(typeof(SplineContainer))] 
    public class EnemyPath : MonoBehaviour
    {
        private SplineContainer _path;

        private void Awake()
        {
            _path = GetComponent<SplineContainer>();
        }

        public bool IsClosed => _path.Splines[0].Closed;

        public Vector3 GetPosition(float progress)
        {
            return _path.EvaluatePosition(progress);
        }

        public Quaternion GetRotation(float progress)
        {
            var tangent = _path.EvaluateTangent(progress);
            var upVector = _path.EvaluateUpVector(progress);

            return Quaternion.LookRotation(tangent, upVector);
        }
    }
}