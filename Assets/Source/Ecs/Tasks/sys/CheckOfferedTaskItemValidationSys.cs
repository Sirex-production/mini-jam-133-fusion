using Secs;

namespace Ingame.Tasks
{
    public sealed class CheckOfferedTaskItemValidationSys : IEcsRunSystem
    {
        [EcsInject]
        private readonly EcsWorld _world;
        
        [EcsInject( typeof(TaskHolderMdl),typeof(OfferTaskItemReq))]
        private readonly EcsFilter _taskFilter;
        
        [EcsInject]
        private readonly EcsPool<TaskHolderMdl> _taskPool;
        
        [EcsInject]
        private readonly EcsPool<OfferTaskItemReq> _offerItemPool;
        
        public void OnRun()
        {
            foreach (var taskEntity in _taskFilter)
            {
                ref var offerTaskItemReq = ref _offerItemPool.GetComponent(taskEntity);
                ref var taskHolderMdl = ref _taskPool.GetComponent(taskEntity);

                if (offerTaskItemReq.offeredItem == taskHolderMdl.currentTask.QuestItem)
                {
                   //TODO wallet + destroy the item
                }
                
                _world.DelEntity(taskEntity);   
            }
        }
    }
}