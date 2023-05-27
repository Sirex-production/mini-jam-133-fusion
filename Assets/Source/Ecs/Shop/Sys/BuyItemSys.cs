using Ingame.Recipe;
using Secs;

namespace Ingame.Shop
{
    public sealed class BuyItemSys : IEcsRunSystem
    {
        [EcsInject] 
        private readonly EcsWorld _ecsWorld;
        
        [EcsInject(typeof(WalletCmp))] 
        private readonly EcsFilter _walletCmpFilter;

        //itemConfig
        [EcsInject(typeof(LockedCardCmp),typeof(TryUnlockCardReq))] 
        private readonly EcsFilter _cardFilter;
        
        [EcsInject] 
        private readonly EcsPool<LockedCardCmp> _lockedCardPool;
        
        [EcsInject] 
        private readonly EcsPool<TryUnlockCardReq> _tryUnlockCardPool;
        
        [EcsInject] 
        private readonly EcsPool<WalletCmp> _walletCmpPool;
        
        [EcsInject] 
        private readonly EcsPool<DiscoverNewItemEvent> _discoverNewItemEventPool;
        
        public void OnRun()
        {

            foreach (var cardEntity in _cardFilter)
            {
                ref var lockedCard = ref _lockedCardPool.GetComponent(cardEntity);
                
                if (_walletCmpFilter.IsEmpty)
                {
                    _tryUnlockCardPool.DelComponent(cardEntity);
                    return;
                }

                var walletEntity = _walletCmpFilter.GetFirstEntity();
                ref var walletCmp  = ref _walletCmpPool.GetComponent(walletEntity);

                if (walletCmp.money >= lockedCard.moneyToUnlock)
                {
                    walletCmp.money -= lockedCard.moneyToUnlock;
                    
                    _lockedCardPool.DelComponent(cardEntity);
                    
                    var newEntity = _ecsWorld.NewEntity();
                    //_discoverNewItemEventPool.AddComponent(newEntity).item = buyItemEvent.requestedItem;
                }
                
                _tryUnlockCardPool.DelComponent(cardEntity);
            }
            
            
            
            /*var buyItemEventEntity = _buyItemEventFilter.GetFirstEntity();
            var walletCmpEntity = _walletCmpFilter.GetFirstEntity();
           
            //ref var buyItemEvent = ref _buyItemEventPool.GetComponent(buyItemEventEntity);
            ref var walletCmp = ref _walletCmpPool.GetComponent(walletCmpEntity);
                    
            if (walletCmp.money >= buyItemEvent.cost)
            {
                walletCmp.money -= buyItemEvent.cost;

                var newEntity = _ecsWorld.NewEntity();
                _discoverNewItemEventPool.AddComponent(newEntity).item = buyItemEvent.requestedItem;
            }
                    
            _ecsWorld.DelEntity(buyItemEventEntity);*/
        }
    }
}