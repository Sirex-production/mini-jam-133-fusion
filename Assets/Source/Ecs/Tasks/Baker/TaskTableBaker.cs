using System;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Tasks
{
    public sealed class TaskTableBaker : MonoBehaviour
    {
        private EcsWorld _ecsWorld;
        [Inject]
        private void Construct(EcsWorldsProvider ecsWorldsProvider)
        {
            _ecsWorld = ecsWorldsProvider.GameplayWorld;
        }

        private void Awake()
        {
            var entity = _ecsWorld.NewEntity();

            _ecsWorld.GetPool<TaskTableTag>().AddComponent(entity);
            _ecsWorld.GetPool<TransformMdl>().AddComponent(entity).transform = transform;
            _ecsWorld.GetPool<OfferedTaskItemsCmp>().AddComponent(entity).offeredItems = new();
            
            this.LinkEcsEntity(_ecsWorld, entity);
            
            Destroy(this); 
        }
    }
}