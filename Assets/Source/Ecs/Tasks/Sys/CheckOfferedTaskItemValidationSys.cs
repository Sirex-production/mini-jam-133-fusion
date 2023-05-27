using Secs;
using UnityEngine;

namespace Ingame.Tasks
{
    public sealed class CheckOfferedTaskItemValidationSys : IEcsRunSystem
    {
        [EcsInject]
        private readonly EcsWorld _world;
        
        [EcsInject( typeof(TaskHolderMdl))]
        private readonly EcsFilter _taskFilter;
        
        [EcsInject( typeof(OfferTaskItemEvent))]
        private readonly EcsFilter _offerTaskItemEventFilter;

        [EcsInject( typeof(OfferedTaskItemsCmp))]
        private readonly EcsFilter _offeredItemsCmpFilter;
        
        [EcsInject( typeof(PlayerWalletCmp))]
        private readonly EcsFilter _walletCmpFilter;
        
        [EcsInject]
        private readonly EcsPool<TaskHolderMdl> _taskPool;
        
        [EcsInject]
        private readonly EcsPool<OfferedTaskItemsCmp> _offerItemsPool;
        
        [EcsInject]
        private readonly EcsPool<AskNewTaskEvent> _askNewTaskPool;
        
        [EcsInject]
        private readonly EcsPool<PlayerWalletCmp> _walletCmpPool;
        
        public void OnRun()
        {
            foreach (var offerTaskItemEventEntity in _offerTaskItemEventFilter)
            {
                if(_offeredItemsCmpFilter.IsEmpty)
                    return;
                
                if(_taskFilter.IsEmpty)
                    return;
                
                if(_walletCmpFilter.IsEmpty)
                    return;
                
                ref var offeredTaskItemsCmp = ref _offerItemsPool.GetComponent(_offeredItemsCmpFilter.GetFirstEntity());
                ref var taskHolderMdl = ref _taskPool.GetComponent(_taskFilter.GetFirstEntity());
                ref var walletCmp = ref _walletCmpPool.GetComponent(_walletCmpFilter.GetFirstEntity());
                
                if (offeredTaskItemsCmp.IsTradeAccepted(taskHolderMdl.currentTask.QuestItems))
                {
                    walletCmp.currentAmountOfCoins += taskHolderMdl.currentTask.Money;
                    
                    var newEntity = _world.NewEntity();
                    _askNewTaskPool.AddComponent(newEntity);
                    
                   
                
                }
                _world.DelEntity(offerTaskItemEventEntity);
            }
        }
    }
}