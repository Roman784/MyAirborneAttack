using Gameplay;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using R3;
using System;
using Object = UnityEngine.Object;

namespace Effects
{
    public class EffectsFacotry : IDisposable
    {
        private readonly CompositeDisposable _disposables = new();

        private Dictionary<string, ObjectPool<Effect>> _effectsMap = new();

        public Effect Create(Effect prefab, Vector3 position, Vector3 normal)
        {
            if (prefab == null) return null;

            if (!_effectsMap.TryGetValue(prefab.NameId, out var pool))
            {
                pool = new ObjectPool<Effect>(
                    createFunc: () => Object.Instantiate(prefab),
                    actionOnGet: (obj) => obj.gameObject.SetActive(true),
                    actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                    defaultCapacity: 10);

                _effectsMap[prefab.NameId] = pool;
            }

            var effect = pool.Get();

            effect.transform.position = position;
            effect.transform.rotation = Quaternion.LookRotation(normal);

            effect.Play()
                .Subscribe(_ => pool.Release(effect))
                .AddTo(_disposables);

            return effect;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}