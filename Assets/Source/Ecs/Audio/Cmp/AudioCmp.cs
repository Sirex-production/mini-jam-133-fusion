using Secs;
using UnityEngine;

namespace Ingame.Audio
{
    public struct AudioCmp : IEcsComponent
    {
        public AudioClip audioClip;
        public AudioSource audioSource;
    }
}