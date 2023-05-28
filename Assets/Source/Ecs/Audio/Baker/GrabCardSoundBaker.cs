using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Audio
{
    public sealed class GrabCardSoundBaker : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        private EcsWorld _ecsWorld;
        
        [Inject]
        private void Construct(EcsWorldsProvider ecsWorldsProvider)
        { 
            _ecsWorld = ecsWorldsProvider.GameplayWorld;
        }
        private void Awake() {
            var newEntity = _ecsWorld.NewEntity();
            
            _ecsWorld.GetPool<AudioCmp>().AddComponent(newEntity).audioClip = audioClip;
            _ecsWorld.GetPool<GrabCardSoundTag>().AddComponent(newEntity);
        }
    }
}