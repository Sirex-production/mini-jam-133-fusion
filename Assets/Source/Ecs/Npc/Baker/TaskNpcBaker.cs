using System;
using System.Collections.Generic;
using DG.Tweening;
using Ingame.Npc;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.NPC
{
    public sealed class TaskNpcBaker : MonoBehaviour
    {
        public Transform startingPosition;
        public Transform endingPosition;
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
            waypointsMdl.transforms = new List<Transform>() { startingPosition, endingPosition };

            entity = _world.NewEntity();
            _world.GetPool<ForwardNpcEvent>().AddComponent(entity);
            //Destroy(this);
        }
    }
}