using R3;
using System;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourcer : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();

            DontDestroyOnLoad(gameObject);
        }

        public Observable<Unit> PlayOneShot(AudioClip clip)
        {
            _audioSource.loop = false;
            _audioSource.PlayOneShot(clip);

            return Observable.Timer(TimeSpan.FromSeconds(clip.length));
        }

        public void PlayLoop(AudioClip clip)
        {
            _audioSource.loop = true;
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        public void SetClip(AudioClip clip)
        {
            _audioSource.clip = clip;
        }

        public void Stop()
        {
            _audioSource.Stop();
            Destroy(gameObject);
        }

        public void Mute()
        {
            _audioSource.mute = true;
        }

        public void Unmute()
        {
            _audioSource.mute = false;
        }
    }
}