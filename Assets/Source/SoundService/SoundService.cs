using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Pool;

namespace Ingame
{
    public sealed class SoundService : MonoBehaviour
    {
        [Required, SerializeField] private AudioClip clickAudioClip;
        
        private ObjectPool<AudioSource> _audioPool;

        private void Awake()
        {
            _audioPool = new ObjectPool<AudioSource>(
                OnAudioClipCreate,
                OnAudioClipGet,
                OnAudioClipRelease,
                OnAudioClipDestroy
                );
        }
        
        private AudioSource OnAudioClipCreate()
        {
            var go = new GameObject("audio");
            go.transform.SetParent(transform);
            return go.AddComponent<AudioSource>();
        }
        
        private void OnAudioClipGet(AudioSource clip)
        {
            clip.gameObject.SetActive(true);
        }

        private void OnAudioClipRelease(AudioSource clip)
        {
            clip.Stop();
            clip.clip = null;
            clip.gameObject.SetActive(false);
        }
        
        private void OnAudioClipDestroy(AudioSource clip)
        {
           
        }

        private IEnumerator PlaySoundAndReturnToPoolRoutine(AudioSource audioSource)
        {
            yield return new WaitUntil(() => !audioSource.isPlaying);

            StopSound(audioSource);
        }
        
        public AudioSource PlaySound(AudioClip clip, bool loop = false)
        {
            var audioSource = _audioPool.Get();
            audioSource.loop = loop;
            audioSource.clip = clip;
            audioSource.Play();
            
            return audioSource;
        }

        public void PlayUiClickSound()
        {
            PlaySoundThenReturnToPool(clickAudioClip);
        }

        public void PlaySoundThenReturnToPool(AudioClip clip)
        {
            var audioSource = _audioPool.Get();
            audioSource.clip = clip;
            audioSource.Play();
            StartCoroutine(PlaySoundAndReturnToPoolRoutine(audioSource));
        }
        
        public void StopSound(AudioSource audioSource)
        {
            audioSource.Stop();
            _audioPool.Release(audioSource);
        }
    }
}