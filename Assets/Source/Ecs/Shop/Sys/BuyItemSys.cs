using Secs;

namespace Ingame.Shop
{
    public sealed class BuyItemSys : IEcsRunSystem
    {
        [EcsInject] 
        private readonly EcsWorld _ecsWorld;
        
        [EcsInject(typeof(BuyItemEvent))] 
        private readonly EcsFilter _buyItemEventFilter;
        
        [EcsInject(typeof(WalletCmp))] 
        private readonly EcsFilter _walletCmpFilter;
        
        [EcsInject] 
        private readonly EcsPool<BuyItemEvent> _buyItemEventPool;
        
        [EcsInject] 
        private readonly EcsPool<WalletCmp> _walletCmpPool;
        
        public void OnRun()
        {
            foreach (var buyItemEventEntity in _buyItemEventFilter)
            {
                ref var buyItemEvent = ref _buyItemEventPool.GetComponent(buyItemEventEntity);
                
                foreach (var walletCmpEntity in _walletCmpFilter)
                {
                    ref var walletCmp = ref _walletCmpPool.GetComponent(walletCmpEntity);
                    
                    if (walletCmp.money >= buyItemEvent.requestedItem.Cost)
                    {
                        walletCmp.money -= buyItemEvent.requestedItem.Cost;
                        //todo add new item to inventory
                    }
                }
                
                _ecsWorld.DelEntity(buyItemEventEntity);
            }
        }
    }
}