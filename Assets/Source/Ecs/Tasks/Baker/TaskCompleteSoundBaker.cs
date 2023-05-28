using Ingame.Audio;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Tasks
{
    public sealed class TaskCompleteSoundBaker : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        private EcsWorld _ecsWorld;
        
        [Inject]
        private void Construct(EcsWorldsProvider ecsWorldsProvider)
        {
            _ecsWorld = ecsWorldsProvider.GameplayWorld;
        }

        private void Awake()
        {
            var newEntity = _ecsWorld.NewEntity();

            _ecsWorld.GetPool<AudioCmp>().AddComponent(newEntity).audioSource = audioSource;
            _ecsWorld.GetPool<TaskCompletedSoundTag>().AddComponent(newEntity);
        }
    }
}