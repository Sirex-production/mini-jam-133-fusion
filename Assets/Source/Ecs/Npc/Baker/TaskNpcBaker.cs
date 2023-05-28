using System;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using Ingame.Audio;
using Ingame.Npc;
using Ingame.Tasks;
using NaughtyAttributes;
using Secs;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Ingame.NPC
{
    public sealed class TaskNpcBaker : MonoBehaviour
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
        
        private void Awake()
        {
            var entity = _world.NewEntity();
            ref var transformMdl = ref _world.GetPool<TransformMdl>().AddComponent(entity);
            
            transformMdl.transform = transform;
            transformMdl.initialLocalPos = transform.localPosition;
            transformMdl.initialLocalRot = transform.localRotation;
            
            _world.GetPool<TaskNpcTag>().AddComponent(entity);
            
            ref var waypointsMdl = ref _world.GetPool<WaypointsCmp>().AddComponent(entity);
            waypointsMdl.transforms = new List<Transform>(positions);

            ref var dialogMdl = ref _world.GetPool<DialogMdl>().AddComponent(entity);
            dialogMdl.text = text;
            dialogMdl.image = dialogBox;
            dialogMdl.taskText = "";
            
            _world.GetPool<RandomAudioCmp>().AddComponent(entity).audioClips = dialogSounds;
            _world.GetPool<TaskHolderMdl>().AddComponent(entity);
            
            gameObject.LinkEcsEntity(_world, entity);
            
            _world.UpdateFilters();
        
            entity = _world.NewEntity();
            _world.GetPool<ForwardNpcEvent>().AddComponent(entity);
            
            Destroy(this);
        }
    }
}