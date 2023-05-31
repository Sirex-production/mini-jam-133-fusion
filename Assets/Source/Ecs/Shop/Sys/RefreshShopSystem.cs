using System.Linq;
using Ingame.Audio;
using Ingame.Recipe;
using Secs;
using UnityEngine;
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
		[EcsInject(typeof(UnlockedItemsMdl))]
		private readonly EcsFilter _unlockedItemsFilter;
		[EcsInject(typeof(PlayerWalletCmp))]
		private readonly EcsFilter _playerWalletFilter;
		[EcsInject(typeof(AudioCmp),typeof(SellItemSoundTag))]
		private readonly EcsFilter _soundFilter;
		
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
		[EcsInject]
		private readonly EcsPool<UnlockedItemsMdl> _unlockedItemsPool;
		[EcsInject]
		private readonly EcsPool<PlayerWalletCmp> _playerWalletPool;
		[EcsInject]
		private readonly EcsPool<AudioCmp> _soundPool;

		private readonly ShopConfig _shopConfig;
		private readonly DiContainer _diContainer;
		private SoundService _soundService;
		public RefreshShopSystem(ShopConfig shopConfig, DiContainer diContainer, SoundService soundService)
		{
			_shopConfig = shopConfig;
			_diContainer = diContainer;
			_soundService = soundService;
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
			if(_unlockedItemsFilter.IsEmpty)
				return;

			if(_refreshShopEventFilter.IsEmpty || _shopFilter.IsEmpty)
				return;

			if(_playerWalletFilter.IsEmpty)
				return;
			
			ref var playerWalletCmp = ref _playerWalletPool.GetComponent(_playerWalletFilter.GetFirstEntity());
			
			if(!playerWalletCmp.HasEnoughCoins(_shopConfig.RefreshCost))
				return;
			
			playerWalletCmp.currentAmountOfCoins -= _shopConfig.RefreshCost;
			ref var shopCmp = ref _shopPool.GetComponent(_shopFilter.GetFirstEntity());

			if (!_soundFilter.IsEmpty)
				_soundService.PlaySoundThenReturnToPool(_soundPool.GetComponent(_soundFilter.GetFirstEntity()).audioClip);

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
				var shopCardEcsBaker = _diContainer.InstantiatePrefabForComponent<EcsMonoBaker>(shopCmp.cardPrefab);
				var shopCardEntityReference = shopCardEcsBaker.GetComponent<EcsEntityReference>();
				
				_world.BakeSpecificBaker(shopCardEcsBaker);

				_shopSlotPool.AddComponent(shopCardEntityReference.EntityId);
			}
		}
		
		private void PlaceCardsInShop(ref ShopCmp shopCmp)
		{
			var unlockedItems = _unlockedItemsPool.GetComponent(_unlockedItemsFilter.GetFirstEntity()).items;
			var randomItems = _shopConfig.GetRandomItems(shopCmp.slotsPositions.Length, unlockedItems).ToArray();
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