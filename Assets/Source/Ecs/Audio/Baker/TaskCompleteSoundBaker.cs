using Ingame.Audio;
using Secs;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Ingame.Tasks
{
    public sealed class TaskCompleteSoundBaker : MonoBehaviour
    {
        [FormerlySerializedAs("audioSource")] [SerializeField] private AudioClip audioClip;
        private EcsWorld _ecsWorld;
        
        [Inject]
        private void Construct(EcsWorldsProvider ecsWorldsProvider)
        {
            _ecsWorld = ecsWorldsProvider.GameplayWorld;
        }

        private void Awake()
        {
            var newEntity = _ecsWorld.NewEntity();

            _ecsWorld.GetPool<AudioCmp>().AddComponent(newEntity).audioClip = audioClip;
            _ecsWorld.GetPool<TaskCompletedSoundTag>().AddComponent(newEntity);
        }
    }
}