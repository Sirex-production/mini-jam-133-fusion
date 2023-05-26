using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Receipt 
{
    public sealed class UnlockNewReceiptsSys : IEcsRunSystem
    {
        [EcsInject(typeof(DiscoverNewRecipeReq))]
        private readonly EcsFilter _newItemFilter;
        
        [EcsInject(typeof(AllRecipesMdl), typeof(RecipeStatusMdl))]
        private readonly EcsFilter _receiptsFilter;
        
        [EcsInject]
        private readonly EcsPool<DiscoverNewRecipeReq> _discoverNewReceiptItemReqPool;
        
        [EcsInject]
        private readonly EcsPool<AllRecipesMdl> _allReceiptsPool;
        
        [EcsInject]
        private readonly EcsPool<RecipeStatusMdl> _receiptStatusPool;
        
        public void OnRun()
        {
            foreach (var newItemEntity in _newItemFilter)
            {
                ref var newItemReq = ref _discoverNewReceiptItemReqPool.GetComponent(newItemEntity);
                var newReceipt = newItemReq.newRecipe;
                
                foreach (var receiptsEntity in _receiptsFilter)
                {
                    ref var receipt = ref _receiptStatusPool.GetComponent(receiptsEntity);
                    
                }
                
                _discoverNewReceiptItemReqPool.DelComponent(newItemEntity);
            }
        }
    }
}