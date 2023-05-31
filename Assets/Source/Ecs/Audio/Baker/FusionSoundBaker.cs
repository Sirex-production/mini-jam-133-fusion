using Ingame.Audio;
using Ingame.Cmp;
using Secs;
using UnityEngine;

namespace Ingame.Baker
{
    public sealed class FusionSoundBaker : EcsMonoBaker
    {
        [SerializeField] private AudioClip audioClip;
        
        protected override void Bake(EcsWorld world, int entityId)
        {
            world.GetPool<AudioCmp>().AddComponent(entityId).audioClip = audioClip;
            world.GetPool<FusionSoundTag>().AddComponent(entityId);
        }
    }
}