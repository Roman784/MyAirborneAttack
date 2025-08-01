using UnityEngine;

namespace Gameplay
{
    public class EnemyView : MonoBehaviour
    {
        public void Move(Vector3 position)
        {
            transform.position = position;
        }

        public void Rotate(Quaternion rotation)
        {
            transform.rotation = rotation;
        }
    }
}