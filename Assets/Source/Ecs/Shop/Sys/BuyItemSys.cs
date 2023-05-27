using Ingame.Recipe;
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
        
        [EcsInject] 
        private readonly EcsPool<DiscoverNewItemEvent> _discoverNewItemEventPool;
        
        public void OnRun()
        {
            
            if(_buyItemEventFilter.IsEmpty || _walletCmpFilter.IsEmpty)
                return;
            
            var buyItemEventEntity = _buyItemEventFilter.GetFirstEntity();
            var walletCmpEntity = _walletCmpFilter.GetFirstEntity();
           
            ref var buyItemEvent = ref _buyItemEventPool.GetComponent(buyItemEventEntity);
            ref var walletCmp = ref _walletCmpPool.GetComponent(walletCmpEntity);
                    
            if (walletCmp.money >= buyItemEvent.requestedItem.Cost)
            {
                walletCmp.money -= buyItemEvent.requestedItem.Cost;

                var newEntity = _ecsWorld.NewEntity();
                _discoverNewItemEventPool.AddComponent(newEntity).item = buyItemEvent.requestedItem.Item;
            }
                    
            _ecsWorld.DelEntity(buyItemEventEntity);
        }
    }
}