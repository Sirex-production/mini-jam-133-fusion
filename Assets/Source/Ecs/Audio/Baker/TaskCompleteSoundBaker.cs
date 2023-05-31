using Ingame.Audio;
using Secs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ingame.Tasks
{
    public sealed class TaskCompleteSoundBaker : EcsMonoBaker
    {
        [FormerlySerializedAs("audioSource")] [SerializeField] private AudioClip audioClip;
        
        protected override void Bake(EcsWorld world, int entityId)
        {
            world.GetPool<AudioCmp>().AddComponent(entityId).audioClip = audioClip;
            world.GetPool<TaskCompletedSoundTag>().AddComponent(entityId);
        }
    }
}