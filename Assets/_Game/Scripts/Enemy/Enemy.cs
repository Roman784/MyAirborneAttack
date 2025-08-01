using UnityEngine;

namespace Gameplay
{
    // TODO: Temp.
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _pathPassingRate;

        private EnemyPath _path;
        private float _pathPassingProgress;

        public void Init(EnemyPath path)
        {
            _path = path;

            _pathPassingProgress = Random.Range(0f, 1f);
        }

        private void Update()
        {
            _pathPassingProgress = Mathf.Repeat(_pathPassingProgress + Time.deltaTime / _pathPassingRate, 1f);

            var pos = _path.EvaluatePosition(_pathPassingProgress);
            var tangent = _path.EvaluateTangent(_pathPassingProgress);
            var up = _path.EvaluateUpVector(_pathPassingProgress);

            transform.position = pos;
            transform.rotation = Quaternion.LookRotation(tangent, up);
        }
    }
}