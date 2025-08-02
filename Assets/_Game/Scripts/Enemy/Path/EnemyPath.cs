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

        public Vector3 EvaluatePosition(float progress) => _path.EvaluatePosition(progress);
        public Vector3 EvaluateTangent(float progress) => _path.EvaluateTangent(progress);
        public Vector3 EvaluateUpVector(float progress) => _path.EvaluateUpVector(progress);
    }
}