using System.Linq;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Recipe 
{
    public sealed class UnlockNewRecipeSys : IEcsRunSystem
    {
        [EcsInject(typeof(DiscoverNewRecipeEvent))]
        private readonly EcsFilter _newItemFilter;
        
        [EcsInject(typeof(RecipeStatusMdl))]
        private readonly EcsFilter _recipeStatusMdlFilter;
        
        [EcsInject( typeof(AllRecipesMdl))]
        private readonly EcsFilter _allReceiptsFilter;
        
        [EcsInject(typeof(UnlockedItemsMdl))]
        private readonly EcsFilter _unlockedItemsFilter;
        
        [EcsInject]
        private readonly EcsPool<DiscoverNewRecipeEvent> _discoverNewReceiptItemEventPool;
        
        [EcsInject]
        private readonly EcsPool<AllRecipesMdl> _allReceiptsPool;
        
        [EcsInject]
        private readonly EcsPool<RecipeStatusMdl> _receiptStatusPool;
        
        [EcsInject]
        private readonly EcsPool<UnlockedItemsMdl> _unlockedItemsPool;
        
        public void OnRun()
        {
            foreach (var newRecipeEntity in _newItemFilter)
            {
                ref var newItemReq = ref _discoverNewReceiptItemEventPool.GetComponent(newRecipeEntity);
                var newRecipe = newItemReq.newRecipe;
                
                foreach (var recipeEntity in _recipeStatusMdlFilter)
                {
                    ref var recipeStatusMdl = ref _receiptStatusPool.GetComponent(recipeEntity);
                    var unlockedRecipes = recipeStatusMdl.unlockedRecipe;
                    
                    if( recipeStatusMdl.discoveredRecipe.Contains(newRecipe))
                        continue;
                    
                    recipeStatusMdl.discoveredRecipe.Add(newRecipe);
                    
                    foreach (var allRecipeEntity in _allReceiptsFilter)
                    {
                        ref var allRecipesMdl = ref _allReceiptsPool.GetComponent(allRecipeEntity);
                        var allRecipe = allRecipesMdl.AllRecipeContainerConfig.AllRecipe;

                        foreach (var unlockedItemsEntity in _unlockedItemsFilter)
                        {
                            ref var unlockedItemsMdl = ref _unlockedItemsPool.GetComponent(unlockedItemsEntity);
                            var unlockedItemsList = unlockedItemsMdl.items;
                            
                            unlockedItemsList.Add(newRecipe.CreatedItem);
                            
                            recipeStatusMdl.unlockedRecipe = allRecipe
                                .Where(e => 
                                    unlockedItemsList.Contains(e.ComponentA) &&
                                    unlockedItemsList.Contains(e.ComponentB))
                                .Where(e=> !unlockedRecipes.Contains(e))
                                .ToHashSet();
                        }
                    }
                }
                
                _discoverNewReceiptItemEventPool.DelComponent(newRecipeEntity);
            }
        }
    }
}