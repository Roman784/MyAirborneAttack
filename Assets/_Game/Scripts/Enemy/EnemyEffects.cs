using Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class EnemyEffects : MonoBehaviour
    {
        [SerializeField] private Effect _explosionEffect;

        public void PlayExplosionEffect()
        {
            Instantiate(_explosionEffect, transform.position, Quaternion.identity).Play();
        }
    }
}