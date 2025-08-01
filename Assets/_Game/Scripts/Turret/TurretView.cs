using System;
using UnityEngine;

namespace Gameplay
{
    public class TurretView : MonoBehaviour
    {
        public T Get<T>() where T : Component
        {
            T component = GetComponent<T>();

            if (component == null)
                throw new ArgumentNullException($"Failed to get {typeof(T)}!");

            return component;
        }
    }
}