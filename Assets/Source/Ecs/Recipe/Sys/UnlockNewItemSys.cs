using Secs;

namespace Ingame.Recipe
{
    public sealed class UnlockNewItemSys : IEcsRunSystem
    {
        [EcsInject] private readonly EcsWorld _ecsWorld;
        
        
        [EcsInject(typeof(RecipeStatusMdl))]
        private readonly EcsFilter _recipeStatusMdlFilter;
        
        [EcsInject( typeof(AllRecipesMdl))]
        private readonly EcsFilter _allReceiptsFilter;
        
        [EcsInject(typeof(UnlockedItemsMdl))]
        private readonly EcsFilter _unlockedItemsFilter;
        
        [EcsInject(typeof(DiscoverNewItemReq))]
        private readonly EcsFilter _discoverNewItemReqFilter;
        
        
        [EcsInject]
        private readonly EcsPool<DiscoverNewItemReq> _discoverNewItemReqPool;
        
        [EcsInject]
        private readonly EcsPool<AllRecipesMdl> _allReceiptsPool;
        
        [EcsInject]
        private readonly EcsPool<RecipeStatusMdl> _receiptStatusPool;
        
        [EcsInject]
        private readonly EcsPool<UnlockedItemsMdl> _unlockedItemsPool;
        
        public void OnRun()
        {
            foreach (var discoverNewItemEntity in _discoverNewItemReqFilter)
            {
                
            }
        }
    }
}