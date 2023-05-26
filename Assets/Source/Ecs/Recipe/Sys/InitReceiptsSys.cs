using System.Collections.Generic;
using System.Linq;
using Secs;
using UnityEngine;

namespace Ingame.Receipt
{
    public sealed class InitReceiptsSys : IEcsInitSystem
    {
        [EcsInject] private EcsWorld _ecsWorld;

        [EcsInject(typeof(AllRecipesMdl))] 
        private readonly EcsFilter _allReceiptsFilter;
        
        [EcsInject(typeof(StartingItemsMdl), typeof(RecipeStatusMdl))]
        private readonly EcsFilter _playerRecipeFilter;
        
        [EcsInject]
        private readonly EcsPool<AllRecipesMdl> _allReceiptsPool;
        
        [EcsInject]
        private readonly EcsPool<StartingItemsMdl> _startingItemsPool;

        [EcsInject]
        private readonly EcsPool<RecipeStatusMdl> _recipeStatusPool;
        
        public void OnInit()
        {   
            foreach (var startingItemsEntity in _playerRecipeFilter)
            {
                ref var startingItemMdl = ref _startingItemsPool.GetComponent(startingItemsEntity);
                ref var recipeStatusMdl = ref _recipeStatusPool.GetComponent(startingItemsEntity);
                
                var startingItems = startingItemMdl.startingItems;
                
                foreach (var receiptsEntity in _allReceiptsFilter)
                {
                    ref var allReceiptsMdl = ref _allReceiptsPool.GetComponent(receiptsEntity);
                    
                    var allRecipe = allReceiptsMdl.AllRecipeContainerConfig.AllRecipe;
                    
                    recipeStatusMdl.discoveredRecipe = new List<Recipe>();

                    var unlockedReceipts = allRecipe.Where(e =>
                        startingItems.Contains(e.ComponentA) && startingItems.Contains(e.ComponentB)).ToList();
                    
                    recipeStatusMdl.unlockedRecipe = unlockedReceipts;
                }
            }
        }
    }
}