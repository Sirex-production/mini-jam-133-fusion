using Secs;

namespace Ingame
{
	public sealed class UpdateCardsViewSystem : IEcsInitSystem, IEcsRunSystem
	{
		[EcsInject]
		private readonly EcsWorld _world;
		
		[EcsInject(typeof(CardViewMdl), typeof(CardCmp))]
		private readonly EcsFilter _cardViewFilter;
		[EcsInject(typeof(UpdateCardsViewEvent))]
		private readonly EcsFilter _updateCardsViewEventFilter;
		[EcsInject(typeof(PlayerWalletCmp))]
		private readonly EcsFilter _playerWalletFilter;
		
		[EcsInject]
		private readonly EcsPool<PlayerWalletCmp> _playerWalletPool;
		[EcsInject]
		private readonly EcsPool<CardViewMdl> _cardViewPool;
		[EcsInject]
		private readonly EcsPool<CardCmp> _cardPool;
		[EcsInject]
		private readonly EcsPool<UpdateCardsViewEvent> _updateCardsViewEventPool;
		[EcsInject]
		private readonly EcsPool<ShopSlotCmp> _shopSlotCmpPool;

		public void OnInit()
		{
			_updateCardsViewEventPool.AddComponent(_world.NewEntity());
		}
		
		public void OnRun()
		{
			if(_updateCardsViewEventFilter.IsEmpty)
				return;

			_updateCardsViewEventPool.DelComponent(_updateCardsViewEventFilter.GetFirstEntity());
			
			foreach(int entity in _cardViewFilter)
			{
				ref var cardViewMdl = ref _cardViewPool.GetComponent(entity);
				ref var cardMdl	= ref _cardPool.GetComponent(entity);
				
				cardViewMdl.cardView.UpdateCardView(cardMdl.itemConfig);

				if(!_shopSlotCmpPool.HasComponent(entity))
				{
					cardViewMdl.cardView.TurnOffShopView();
					continue;
				}

				if(_playerWalletFilter.IsEmpty)
					continue;

				ref var shopSlotCmp = ref _shopSlotCmpPool.GetComponent(entity);
				ref var playerWalletCmp = ref _playerWalletPool.GetComponent(_playerWalletFilter.GetFirstEntity());
				
				cardViewMdl.cardView.TurnOnShopView(shopSlotCmp.price, playerWalletCmp.HasEnoughCoins(shopSlotCmp.price));
			}
		}
	}
}