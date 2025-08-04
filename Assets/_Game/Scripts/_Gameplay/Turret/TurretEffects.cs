using Effects;
using UnityEngine;

namespace Gameplay
{
    public class TurretEffects : MonoBehaviour
    {
        [SerializeField] private Effect _explosionEffect;

        public void PlayExplosionEffect()
        {
            Instantiate(_explosionEffect, transform.position, Quaternion.identity).Play();
        }
    }
}