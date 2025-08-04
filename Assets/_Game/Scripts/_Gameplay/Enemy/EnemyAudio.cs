using Audio;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class EnemyAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _movementSound;
        [SerializeField] private AudioClip _explosionSound;

        private AudioProvider _audioProvider;

        private AudioSourcer _currentSourcer;

        [Inject]
        private void Construct(AudioProvider audioProvider)
        {
            _audioProvider = audioProvider;
        }

        public void Mute()
        {
            _currentSourcer.Mute();
        }

        public void Unmute()
        {
            _currentSourcer.Unmute();
        }

        public void PlayMovementSound()
        {
            _currentSourcer = _audioProvider.PlayLoop(_movementSound, transform);
        }

        public void PlayExplosionSound()
        {
            _currentSourcer.Stop();
            _audioProvider.PlayOneShot(_explosionSound, transform.position);
        }
    }
}