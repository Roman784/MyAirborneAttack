using R3;
using System;
using UnityEngine;

namespace Effects
{
    public class Effect : MonoBehaviour
    {
        [field: SerializeField] public string NameId { get; private set; }

        [SerializeField] private ParticleSystem _particleSystem;

        public Observable<Unit> Play()
        {
            _particleSystem.Play();

            return Observable.Timer(TimeSpan.FromSeconds(_particleSystem.main.duration));
        }
    }
}