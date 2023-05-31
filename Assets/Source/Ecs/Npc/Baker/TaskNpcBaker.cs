using System.Collections.Generic;
using Ingame.Audio;
using Ingame.Npc;
using Ingame.Tasks;
using NaughtyAttributes;
using Secs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ingame.NPC
{
    public sealed class TaskNpcBaker : EcsMonoBaker
    {
        [Required]
        [SerializeField] private List<AudioClip> dialogSounds;
        
        [SerializeField]
        private List<Transform> positions;
        
        [Required]
        [SerializeField]
        private TextMeshProUGUI text;
        
        [Required]
        [SerializeField] 
        private Image dialogBox;
        
        private EcsWorld _world;
        
        [Inject]
        private void Construct(EcsWorldsProvider ecsWorldsProvider)
        {
            _world = ecsWorldsProvider.GameplayWorld;
        }

        protected override void Bake(EcsWorld world, int entityId)
        {
            ref var transformMdl = ref _world.GetPool<TransformMdl>().AddComponent(entityId);
            
            transformMdl.transform = transform;
            transformMdl.initialLocalPos = transform.localPosition;
            transformMdl.initialLocalRot = transform.localRotation;
            
            _world.GetPool<TaskNpcTag>().AddComponent(entityId);
            
            ref var waypointsMdl = ref _world.GetPool<WaypointsCmp>().AddComponent(entityId);
            waypointsMdl.transforms = new List<Transform>(positions);

            ref var dialogMdl = ref _world.GetPool<DialogMdl>().AddComponent(entityId);
            dialogMdl.text = text;
            dialogMdl.image = dialogBox;
            dialogMdl.taskText = "";
            
            _world.GetPool<RandomAudioCmp>().AddComponent(entityId).audioClips = dialogSounds;
            _world.GetPool<TaskHolderMdl>().AddComponent(entityId);

            entityId = _world.NewEntity();
            _world.GetPool<ForwardNpcEvent>().AddComponent(entityId);
        }
    }
}