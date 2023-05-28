using System.Collections.Generic;
using System.Linq;
using Secs;
using Zenject;

namespace Ingame
{
	public sealed class RefreshShopSystem : IEcsInitSystem, IEcsRunSystem
	{
		[EcsInject]
		private readonly EcsWorld _world;
		
		[EcsInject(typeof(RefreshShopEvent))]
		private readonly EcsFilter _refreshShopEventFilter;
		[EcsInject(typeof(ShopCmp))]
		private readonly EcsFilter _shopFilter;
		[EcsInject(typeof(TransformMdl), typeof(CardCmp), typeof(ShopSlotCmp))]
		private readonly EcsFilter _shopSlotFilter;

		[EcsInject]
		private readonly EcsPool<RefreshShopEvent> _refreshShopEventPool;
		[EcsInject]
		private readonly EcsPool<ShopCmp> _shopPool;
		[EcsInject]
		private readonly EcsPool<ShopSlotCmp> _shopSlotPool;
		[EcsInject]
		private readonly EcsPool<CardCmp> _cardPool;
		[EcsInject]
		private readonly EcsPool<TransformMdl> _transformPool;
		[EcsInject]
		private readonly EcsPool<UpdateCardsViewEvent> _updateCardsViewEventPool;
		[EcsInject]
		private readonly EcsPool<UpdateGameplayUiEvent> _updateGameplayUiEventPool;

		private readonly ShopConfig _shopConfig;
		private readonly DiContainer _diContainer;
		
		public RefreshShopSystem(ShopConfig shopConfig, DiContainer diContainer)
		{
			_shopConfig = shopConfig;
			_diContainer = diContainer;
		}

		public void OnInit()
		{
			int playerWalletEntity = _world.NewEntity();
			_world.GetPool<PlayerWalletCmp>().AddComponent(playerWalletEntity) = new PlayerWalletCmp
			{
				currentAmountOfCoins = _shopConfig.InitialAmountOfCoins
			};
			
			_refreshShopEventPool.AddComponent(_world.NewEntity());
		}
		
		public void OnRun()
		{
			if(_refreshShopEventFilter.IsEmpty || _shopFilter.IsEmpty)
				return;
			
			ref var shopCmp = ref _shopPool.GetComponent(_shopFilter.GetFirstEntity());
			
			InstantiateMissingCards(ref shopCmp);
			PlaceCardsInShop(ref shopCmp);
			
			_refreshShopEventPool.DelComponent(_refreshShopEventFilter.GetFirstEntity());
			_updateCardsViewEventPool.AddComponent(_world.NewEntity());
			_updateGameplayUiEventPool.AddComponent(_world.NewEntity());
		}

		private void InstantiateMissingCards(ref ShopCmp shopCmp)
		{
			int amountOfMissingCardsInShop = shopCmp.slotsPositions.Length - _shopSlotFilter.EntitiesCount;

			for(int i = 0; i < amountOfMissingCardsInShop; i++)
			{
				var shopCardEcsEntityReference = _diContainer.InstantiatePrefabForComponent<EcsEntityReference>(shopCmp.cardPrefab);
				_shopSlotPool.AddComponent(shopCardEcsEntityReference.EntityId);
			}
			
			_world.UpdateFilters();
		}
		
		private void PlaceCardsInShop(ref ShopCmp shopCmp)
		{
			var randomItems = _shopConfig.GetRandomItems(shopCmp.slotsPositions.Length).ToArray();
			int currentSlotIndex = shopCmp.slotsPositions.Length - 1;
			
			foreach(var shopSlotEntity in _shopSlotFilter)
			{
				if(currentSlotIndex < 0)
					return;
				
				ref var cardCmp = ref _cardPool.GetComponent(shopSlotEntity);
				ref var cardSlotCmp = ref _shopSlotPool.GetComponent(shopSlotEntity);
				ref var slotTransformMdl = ref _transformPool.GetComponent(shopSlotEntity);

				cardCmp.itemConfig = randomItems[currentSlotIndex].itemConfig;
				cardSlotCmp.price = randomItems[currentSlotIndex].price;
				cardSlotCmp.slotIndex = currentSlotIndex;
				
				slotTransformMdl.transform.position = shopCmp.slotsPositions[currentSlotIndex].position;
				
				currentSlotIndex--;
			}
		}
	}
}