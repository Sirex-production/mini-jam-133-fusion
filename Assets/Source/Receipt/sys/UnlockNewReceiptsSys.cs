using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Receipt 
{
    public sealed class UnlockNewReceiptsSys : IEcsRunSystem
    {
        [EcsInject(typeof(DiscoverNewReceiptReq))]
        private readonly EcsFilter _newItemFilter;
        
        [EcsInject(typeof(AllReceiptsMdl), typeof(ReceiptStatusMdl))]
        private readonly EcsFilter _receiptsFilter;
        
        [EcsInject]
        private readonly EcsPool<DiscoverNewReceiptReq> _discoverNewReceiptItemReqPool;
        
        [EcsInject]
        private readonly EcsPool<AllReceiptsMdl> _allReceiptsPool;
        
        [EcsInject]
        private readonly EcsPool<ReceiptStatusMdl> _receiptStatusPool;
        
        public void OnRun()
        {
            foreach (var newItemEntity in _newItemFilter)
            {
                ref var newItemReq = ref _discoverNewReceiptItemReqPool.GetComponent(newItemEntity);
                var newReceipt = newItemReq.newReceipt;
                
                foreach (var receiptsEntity in _receiptsFilter)
                {
                    ref var receipt = ref _receiptStatusPool.GetComponent(receiptsEntity);
                    
                }
                
                _discoverNewReceiptItemReqPool.DelComponent(newItemEntity);
            }
        }
    }
}