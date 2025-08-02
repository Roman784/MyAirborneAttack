using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private DamageReceiver[] _damageRecipients;

        public IEnumerable<DamageReceiver> DamageRecipients => _damageRecipients;

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
            transform.position = position;
        }

        public void Rotate(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}