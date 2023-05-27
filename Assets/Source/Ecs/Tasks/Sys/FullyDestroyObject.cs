using System;
using Secs;

namespace Ingame.Tasks
{
    public sealed class FullyDestroyObject : IEcsRunSystem
    {
        [EcsInject]
        private EcsWorld _ecsWorld;
        
        [EcsInject(typeof(FullDestroyObjectRequest))]
        private EcsFilter _ecsFilter;
        
        [EcsInject]
        private EcsPool<FullDestroyObjectRequest> _ecsPool;
        public void OnRun()
        {
            foreach (var entity in _ecsFilter)
            {
                ref var destroyObjectRequest = ref _ecsPool.GetComponent(entity);
                
                UnityEngine.Object.Destroy(destroyObjectRequest.gameObject);
                _ecsWorld.DelEntity(destroyObjectRequest.entityId);
                _ecsWorld.DelEntity(entity);
            }
        }
    }
}