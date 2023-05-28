using Ingame.Audio;
using Ingame.Recipe;
using Secs;
using UnityEngine;

namespace Ingame
{
	public sealed class SellItemSystem : IEcsRunSystem
	{
		[EcsInject]
		private readonly EcsWorld _world;
		
		[EcsInject(typeof(OnTriggerEnterEvent))]
		private readonly EcsFilter _onTriggerEnterEventFilter;
		[EcsInject(typeof(PlayerWalletCmp))]
		private readonly EcsFilter _walletFilter;
		
		[EcsInject(typeof(AudioCmp),typeof(SellItemSoundTag))]
		private readonly EcsFilter _soundFilter;
		
		[EcsInject]
		private readonly EcsPool<PlayerWalletCmp> _walletPool;
		[EcsInject]
		private readonly EcsPool<OnTriggerEnterEvent> _onTriggerEnterEventPool;
		[EcsInject]
		private readonly EcsPool<CraftingSurfaceTag> _craftingSurfacePool;
		[EcsInject]
		private readonly EcsPool<ShopSlotCmp> _shopSlotPool;
		[EcsInject]
		private readonly EcsPool<CardCmp> _cardCmpPool;
		[EcsInject]
		private readonly EcsPool<UpdateCardsViewEvent> _updateCardsViewEventPool;
		[EcsInject]
		private readonly EcsPool<UpdateGameplayUiEvent> _updateGameplayUiEventPool;
		[EcsInject]
		private readonly EcsPool<DiscoverNewItemEvent> _discoverNewItemEventPool;
		[EcsInject]
		private readonly EcsPool<AudioCmp> _soundPool;
		
		private SoundService _soundService;
		public SellItemSystem(SoundService soundService)
		{
			_soundService = soundService;
		}
		
		public void OnRun()
		{
			if(_walletFilter.IsEmpty)
				return;
			
			ref var walletCmp = ref _walletPool.GetComponent(_walletFilter.GetFirstEntity());

			foreach(var entity in _onTriggerEnterEventFilter)
			{
				ref var onTriggerEnterEvent = ref _onTriggerEnterEventPool.GetComponent(entity);
				
				if(onTriggerEnterEvent.collider == null || onTriggerEnterEvent.senderObject == null)
					continue;

				if(!onTriggerEnterEvent.senderObject.TryGetComponent(out EcsEntityReference senderEntityReference))
					continue;

				if(!onTriggerEnterEvent.collider.TryGetComponent(out EcsEntityReference otherEntityReference))
					continue;
				
				if(!_craftingSurfacePool.HasComponent(senderEntityReference.EntityId))
					continue;

				if(!_shopSlotPool.HasComponent(otherEntityReference.EntityId))
					continue;
				
				
				SellItem(otherEntityReference.EntityId, ref walletCmp);
			}
		}
		
		private void SellItem(int cardEntityId, ref PlayerWalletCmp walletCmp)
		{
			ref var shopSlotCmp = ref _shopSlotPool.GetComponent(cardEntityId);
			ref var cardCmp = ref _cardCmpPool.GetComponent(cardEntityId);
			
			if(!walletCmp.HasEnoughCoins(shopSlotCmp.price))
				return;

			walletCmp.currentAmountOfCoins -= shopSlotCmp.price;

			if (!_soundFilter.IsEmpty)
			{
				ref var audioCmp = ref _soundPool.GetComponent(_soundFilter.GetFirstEntity());
				
				if(audioCmp.audioSource != null)
					_soundService.StopSound(audioCmp.audioSource);
				
				audioCmp.audioSource = _soundService.PlaySound(audioCmp.audioClip);
			}
			
			_shopSlotPool.DelComponent(cardEntityId);
			_updateCardsViewEventPool.AddComponent(_world.NewEntity());
			_updateGameplayUiEventPool.AddComponent(_world.NewEntity());
			_discoverNewItemEventPool.AddComponent(_world.NewEntity()).item = cardCmp.itemConfig;
		}
	}
}