using System.Linq;
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
        
        [EcsInject(typeof(DiscoverNewItemEvent))]
        private readonly EcsFilter _discoverNewItemFilter;
        
        
        [EcsInject]
        private readonly EcsPool<DiscoverNewItemEvent> _discoverNewItemPool;
        
        [EcsInject]
        private readonly EcsPool<AllRecipesMdl> _allReceiptsPool;
        
        [EcsInject]
        private readonly EcsPool<RecipeStatusMdl> _recipeStatusPool;
        
        [EcsInject]
        private readonly EcsPool<UnlockedItemsMdl> _unlockedItemsPool;
        
        public void OnRun()
        {
            if(_discoverNewItemFilter.IsEmpty)
                return;

            if(_unlockedItemsFilter.IsEmpty || _recipeStatusMdlFilter.IsEmpty || _allReceiptsFilter.IsEmpty)
                return;
            
            var discoverNewItemEntity = _discoverNewItemFilter.GetFirstEntity();
            var allReceiptsEntity = _allReceiptsFilter.GetFirstEntity();
            var recipeStatusMdlEntity = _recipeStatusMdlFilter.GetFirstEntity();
            var unlockedItemsEntity = _unlockedItemsFilter.GetFirstEntity();
            
            
            ref var discoverNewItemEvent = ref _discoverNewItemPool.GetComponent(discoverNewItemEntity);
            var discoverNewItem = discoverNewItemEvent.item;

            ref var unlockedItem = ref _unlockedItemsPool.GetComponent(unlockedItemsEntity);
            var unlockedItemsList = unlockedItem.items;
            
            unlockedItemsList.Add(discoverNewItem);

            ref var allItems = ref _allReceiptsPool.GetComponent(allReceiptsEntity);
            var allItemList = allItems.AllRecipeContainerConfig.AllRecipe;

            ref var recipeStatus = ref _recipeStatusPool.GetComponent(recipeStatusMdlEntity);
            var alreadyDiscoveredRecipes =  recipeStatus.discoveredRecipe;
            
            var newUnlockedItems =  allItemList
                .Where(e => unlockedItemsList.Contains(e.ComponentA) && unlockedItemsList.Contains(e.ComponentB))
                .Where(e=>alreadyDiscoveredRecipes.Contains(e))
                .ToHashSet();

            recipeStatus.discoveredRecipe = newUnlockedItems;
        }
    }
}