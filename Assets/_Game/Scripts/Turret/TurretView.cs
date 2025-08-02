using System;
using UnityEngine;

namespace Gameplay
{
    public class TurretView : MonoBehaviour
    {
        [field: SerializeField] public Transform CameraAnchor { get; private set; }
        [field: SerializeField] public DamageReceiver DamageReceiver { get; private set; }

        public Vector3 Position => transform.position;

        public T Get<T>() where T : Component
        {
            T component = GetComponent<T>();

            if (component == null)
                throw new ArgumentNullException($"Failed to get {typeof(T)}!");

            return component;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}