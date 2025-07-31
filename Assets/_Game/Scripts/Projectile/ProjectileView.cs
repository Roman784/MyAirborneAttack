using UnityEngine;

namespace Gameplay
{
    public class ProjectileView : MonoBehaviour
    {
        [field: SerializeField] public string NameId { get; set; }

        public Vector3 Position => transform.position;
        public Vector3 PreviousPosition { get; private set; }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Move(Vector3 position)
        {
            PreviousPosition = position;
            transform.position = position;
        }

        public void Rotate(Quaternion rotation)
        {
            transform.rotation = rotation;
        }
    }
}