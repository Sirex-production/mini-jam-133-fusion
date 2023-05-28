using Secs;
using UnityEngine;


namespace Ingame.Tasks
{
    public sealed class DetectCollisionWithTasksTable : IEcsRunSystem
    {
        [EcsInject] private EcsWorld _ecsWorld;

        [EcsInject(typeof(OnTriggerEnterEvent))] 
        private EcsFilter _onTriggerEnterFilter;
        
        [EcsInject(typeof(OnTriggerExitEvent))] 
        private EcsFilter _onTriggerExitFilter;

        [EcsInject(typeof(OfferedTaskItemsCmp))] 
        private EcsFilter _offeredTaskItemsCmpFilter;
        
        [EcsInject]
        private EcsPool<OnTriggerEnterEvent> _onTriggerEnterPool;
        [EcsInject]
        private EcsPool<OnTriggerExitEvent> _onTriggerExitPool;
        [EcsInject]
        private EcsPool<OfferedTaskItemsCmp> _offeredTaskItemsCmpPool;
        [EcsInject]
        private EcsPool<OfferTaskItemEvent> _offerTaskItemEventPool;
        
        public void OnRun()
        {
            foreach (var onTriggerEnterEntity in _onTriggerEnterFilter)
            {
                if (_offeredTaskItemsCmpFilter.IsEmpty)
                    return;
       
                var offeredTaskItemEntity = _offeredTaskItemsCmpFilter.GetFirstEntity();
                ref var offeredTaskItems = ref _offeredTaskItemsCmpPool.GetComponent(offeredTaskItemEntity);
                
                ref var onTriggerEnterEvent = ref _onTriggerEnterPool.GetComponent(onTriggerEnterEntity);

                if (!onTriggerEnterEvent.senderObject.TryGetComponent<EcsEntityReference>(out var tableEntityReference))
                    continue;

                if(!tableEntityReference.World.GetPool<TaskTableTag>().HasComponent(tableEntityReference.EntityId))
                    continue;
                
                if (!onTriggerEnterEvent.collider.TryGetComponent<EcsEntityReference>(out var cardEntityReference))
                    continue;
                
                if(!cardEntityReference.World.GetPool<CardCmp>().HasComponent(cardEntityReference.EntityId))
                    continue;

                if(cardEntityReference.World.GetPool<ShopSlotCmp>().HasComponent(cardEntityReference.EntityId))
                    continue;
                
                ref var cardCmp = ref cardEntityReference.World.GetPool<CardCmp>().GetComponent(cardEntityReference.EntityId);
                ref var transformMdlCmp = ref cardEntityReference.World.GetPool<TransformMdl>().GetComponent(cardEntityReference.EntityId);
                
                offeredTaskItems.Add(cardCmp.itemConfig,transformMdlCmp.transform);

                var newEntity = _ecsWorld.NewEntity();
                _offerTaskItemEventPool.AddComponent(newEntity);
            }
            
            
            foreach (var onTriggerExitEntity in _onTriggerExitFilter)
            {
                if (_offeredTaskItemsCmpFilter.IsEmpty)
                    return;

                ref var offeredTaskItems =
                    ref _offeredTaskItemsCmpPool.GetComponent(_offeredTaskItemsCmpFilter.GetFirstEntity());
                
                ref var onTriggerExitEvent = ref _onTriggerExitPool.GetComponent(onTriggerExitEntity);

                if (!onTriggerExitEvent.senderObject.TryGetComponent<EcsEntityReference>(out var tableEntityReference))
                    continue;

                if(!tableEntityReference.World.GetPool<TaskTableTag>().HasComponent(tableEntityReference.EntityId))
                    continue;
                
                if (!onTriggerExitEvent.collider.TryGetComponent<EcsEntityReference>(out var cardEntityReference))
                    continue;
                
                if(!cardEntityReference.World.GetPool<CardCmp>().HasComponent(cardEntityReference.EntityId))
                    continue;
                
                if(cardEntityReference.World.GetPool<ShopSlotCmp>().HasComponent(cardEntityReference.EntityId))
                    continue;
                
                ref var cardCmp = ref cardEntityReference.World.GetPool<CardCmp>().GetComponent(cardEntityReference.EntityId);
                ref var transformMdlCmp = ref cardEntityReference.World.GetPool<TransformMdl>().GetComponent(cardEntityReference.EntityId);
                
                offeredTaskItems.Remove(cardCmp.itemConfig,transformMdlCmp.transform);
                
                var newEntity = _ecsWorld.NewEntity();
                _offerTaskItemEventPool.AddComponent(newEntity);
            }
        }
    }
}