using System.Collections.Generic;
using System.Linq;
using Secs;
using UnityEngine;

namespace Ingame.Receipt
{
    public sealed class InitReceiptsSys : IEcsInitSystem
    {
        private readonly EcsFilter _startingItemsFilter;
        
        [EcsInject(typeof(AllReceiptsMdl), typeof(ReceiptStatusMdl))]
        private readonly EcsFilter _receiptsFilter;
        
        [EcsInject]
        private readonly EcsPool<StartingItemsMdl> _startingItemsPool;
        
        [EcsInject]
        private readonly EcsPool<AllReceiptsMdl> _allReceiptsPool;
              
        [EcsInject]
        private readonly EcsPool<ReceiptStatusMdl> _receiptStatusPool;

        public InitReceiptsSys(EcsWorld ecsWorld)
        {
            var matcher = EcsMatcher
                .Include(typeof(AllReceiptsMdl))
                // .Exclude(typeof(StartingItemsMdl))
                .End();

            _startingItemsFilter = ecsWorld.GetFilter(matcher);
        }
        public void OnInit()
        {   
            foreach (var startingItemsEntity in _startingItemsFilter)
            { 
                Debug.Log(123);
                ref var startingItemMdl = ref _startingItemsPool.GetComponent(startingItemsEntity);
                var startingItems = startingItemMdl.startingItems;
                
                foreach (var receiptsEntity in _receiptsFilter)
                {
                    ref var allReceiptsMdl = ref _allReceiptsPool.GetComponent(receiptsEntity);
                    ref var receiptStatusMdl = ref _receiptStatusPool.GetComponent(receiptsEntity);
                    
                    var allReceipts = allReceiptsMdl.allReceiptsContainerConfig.AllReceipts;
                    
                    receiptStatusMdl.discoveredReceipts = new List<Receipt>();

                    var unlockedReceipts = allReceipts.Where(e =>
                        startingItems.Contains(e.ComponentA) && startingItems.Contains(e.ComponentB)).ToList();
                    
                    receiptStatusMdl.unlockedReceipts = unlockedReceipts;
                }
            }
        }
    }
}