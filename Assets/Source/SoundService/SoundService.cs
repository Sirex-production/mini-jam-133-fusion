using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Ingame
{
    public sealed class SoundService : MonoBehaviour
    {
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
            return go.AddComponent<AudioSource>();
        }
      

        private void OnAudioClipGet(AudioSource clip)
        {
            clip.gameObject.SetActive(true);
        }

        private void OnAudioClipRelease(AudioSource clip)
        {
            clip.Stop();
            clip.gameObject.SetActive(false);
        }
        
        private void OnAudioClipDestroy(AudioSource clip)
        {
           
        }

        public AudioSource PlaySound(AudioClip clip, bool loop = false)
        {
            var audioSource = _audioPool.Get();
            audioSource.loop = loop;
            audioSource.clip = clip;
            audioSource.Play();
            
            return audioSource;
        }
        
        public void StopSound(AudioSource audioSource)
        {
            audioSource.Stop();
            _audioPool.Release(audioSource);
        }
    }
}