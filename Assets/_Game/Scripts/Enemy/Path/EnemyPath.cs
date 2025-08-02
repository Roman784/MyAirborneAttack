using UnityEngine;
using UnityEngine.Splines;

namespace Gameplay
{
    [RequireComponent(typeof(SplineContainer))] 
    public class EnemyPath : MonoBehaviour
    {
        private SplineContainer _path;
        
        private SplineContainer path
        {
            get { return _path ??= GetComponent<SplineContainer>(); }
        }

        public bool IsClosed => path.Splines[0].Closed;

        public Vector3 EvaluatePosition(float progress) => path.EvaluatePosition(progress);
        public Vector3 EvaluateTangent(float progress) => path.EvaluateTangent(progress);
        public Vector3 EvaluateUpVector(float progress) => path.EvaluateUpVector(progress);
    }
}