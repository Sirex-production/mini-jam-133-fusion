using System.Collections.Generic;
using Secs;
using UnityEngine;

namespace Ingame.Audio
{
    public struct RandomAudioCmp : IEcsComponent
    {
        public List<AudioClip> audioClips;

        public AudioClip GetRandom()=> audioClips[Random.Range(0,audioClips.Count)];
        
    }
}