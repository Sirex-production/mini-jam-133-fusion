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
        private readonly EcsFilter _startingItemsFilter;
        
        [EcsInject(typeof(AllRecipesMdl), typeof(RecipeStatusMdl))]
        private readonly EcsFilter _receiptsFilter;
        
        [EcsInject]
        private readonly EcsPool<StartingItemsMdl> _startingItemsPool;
        
        [EcsInject]
        private readonly EcsPool<AllRecipesMdl> _allReceiptsPool;
              
        [EcsInject]
        private readonly EcsPool<RecipeStatusMdl> _receiptStatusPool;
        
        public void OnInit()
        {   
            foreach (var startingItemsEntity in _startingItemsFilter)
            {
                ref var startingItemMdl = ref _startingItemsPool.GetComponent(startingItemsEntity);
                var startingItems = startingItemMdl.startingItems;
                
                foreach (var receiptsEntity in _receiptsFilter)
                {
                    ref var allReceiptsMdl = ref _allReceiptsPool.GetComponent(receiptsEntity);
                    ref var receiptStatusMdl = ref _receiptStatusPool.GetComponent(receiptsEntity);
                    
                    var allReceipts = allReceiptsMdl.AllRecipeContainerConfig.AllRecipe;
                    
                    receiptStatusMdl.discoveredRecipe = new List<Recipe>();

                    var unlockedReceipts = allReceipts.Where(e =>
                        startingItems.Contains(e.ComponentA) && startingItems.Contains(e.ComponentB)).ToList();
                    
                    receiptStatusMdl.unlockedRecipe = unlockedReceipts;
                }
            }
        }
    }
}