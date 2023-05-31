using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Audio
{
    public sealed class GrabCardSoundBaker : EcsMonoBaker
    {
        [SerializeField] private AudioClip audioClip;

        protected override void Bake(EcsWorld world, int entityId)
        {
            world.GetPool<AudioCmp>().AddComponent(entityId).audioClip = audioClip;
            world.GetPool<GrabCardSoundTag>().AddComponent(entityId);
        }
    }
}