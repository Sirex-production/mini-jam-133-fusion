using System;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Ingame.Audio
{
    public sealed class PlayMusic : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float musicVolume;
        [Required, SerializeField] private AudioClip musicClip;
        private SoundService _soundService;
        
        [Inject]
        private void Construct(SoundService soundService)
        {
            _soundService = soundService;
        }

        private void Start()
        {
           var audioSource = _soundService.PlaySound(musicClip, true);
           audioSource.volume = musicVolume;
        }
    }
}