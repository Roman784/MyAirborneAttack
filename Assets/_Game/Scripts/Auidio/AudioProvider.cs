using R3;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Audio
{
    public class AudioProvider
    {
        private readonly AudioSourcer _sourcerPrefab;
        private readonly ObjectPool<AudioSourcer> _sourcersPool;

        [Inject]
        public AudioProvider(AudioSourcer sourcerPrefab)
        {
            _sourcerPrefab = sourcerPrefab;
            _sourcersPool = new(
                createFunc: () => Object.Instantiate(_sourcerPrefab),
                actionOnGet: (obj) => obj.gameObject.SetActive(true),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                defaultCapacity: 10);
        }

        public AudioSourcer PlayOneShot(AudioClip audioClip, Vector3 position = default)
        {
            var sourcer = _sourcersPool.Get();
            sourcer.transform.position = position;

            sourcer.PlayOneShot(audioClip)
                .Subscribe(_ => _sourcersPool.Release(sourcer));

            return sourcer;
        }

        public AudioSourcer PlayLoop(AudioClip audioClip, Transform parent = null)
        {
            var sourcer = Object.Instantiate(_sourcerPrefab);

            if (parent != null)
            {
                sourcer.transform.SetParent(parent, false);
                sourcer.transform.localPosition = Vector3.zero;
            }

            sourcer.PlayLoop(audioClip);

            return sourcer;
        }
    }
}