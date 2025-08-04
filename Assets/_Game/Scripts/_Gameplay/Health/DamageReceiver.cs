using System;
using UnityEngine;
using R3;

namespace Gameplay
{
    public class DamageReceiver : MonoBehaviour
    {
        [SerializeField] private float _damageMultiplyer;

        [Space]

        [SerializeField] private Hitbox _hitbox;

        private Subject<float> _damageSignalSubj = new();
        public Observable<float> DamageSignal => _damageSignalSubj; 

        private void Start()
        {
            _hitbox.OnHitSignal.Subscribe(hitData =>
            {
                float damage = hitData.Damage * _damageMultiplyer;
                TakeDamage(damage);
            });
        }

        private void TakeDamage(float damage)
        {
            _damageSignalSubj.OnNext(damage);
        }
    }
}