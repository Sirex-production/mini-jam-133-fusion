using System;
using Ingame.Audio;
using Ingame.Cmp;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Baker
{
    public sealed class FusionSoundBaker : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        
        private EcsWorld _world;
        
        [Inject]
        private void Construct(EcsWorldsProvider ecsWorldsProvider)
        {
            _world = ecsWorldsProvider.GameplayWorld;
        }

        private void Awake()
        {
            var newEntity = _world.NewEntity();
            
            _world.GetPool<AudioCmp>().AddComponent(newEntity).audioClip = audioClip;
            _world.GetPool<FusionSoundTag>().AddComponent(newEntity);
        }
    }
}