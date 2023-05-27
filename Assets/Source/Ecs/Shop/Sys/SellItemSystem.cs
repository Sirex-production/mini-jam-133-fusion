﻿using Secs;
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
		
		[EcsInject]
		private readonly EcsPool<PlayerWalletCmp> _walletPool;
		[EcsInject]
		private readonly EcsPool<OnTriggerEnterEvent> _onTriggerEnterEventPool;
		[EcsInject]
		private readonly EcsPool<CraftingSurfaceTag> _craftingSurfacePool;
		[EcsInject]
		private readonly EcsPool<ShopSlotCmp> _shopSlotPool;
		[EcsInject]
		private readonly EcsPool<UpdateCardsViewEvent> _updateCardsViewEventPool;

		public void OnRun()
		{
			if(_walletFilter.IsEmpty)
				return;
			
			ref var walletCmp = ref _walletPool.GetComponent(_walletFilter.GetFirstEntity());
			
			Debug.Log("A");

			foreach(var entity in _onTriggerEnterEventFilter)
			{
				ref var onTriggerEnterEvent = ref _onTriggerEnterEventPool.GetComponent(entity);

				Debug.Log("A1");
				
				if(!onTriggerEnterEvent.senderObject.TryGetComponent(out EcsEntityReference senderEntityReference))
					continue;
			
				Debug.Log("B");
				
				if(!_craftingSurfacePool.HasComponent(senderEntityReference.EntityId))
					continue;
				
				Debug.Log("C");
				
				if(!onTriggerEnterEvent.collider.TryGetComponent(out EcsEntityReference otherEntityReference))
					continue;
			
				Debug.Log("D");
				
				if(!_shopSlotPool.HasComponent(otherEntityReference.EntityId))
					continue;
				
				Debug.Log("E");
				
				SellItem(otherEntityReference.EntityId, ref walletCmp);
			}
		}
		
		private void SellItem(int cardEntityId, ref PlayerWalletCmp walletCmp)
		{
			ref var shopSlotCmp = ref _shopSlotPool.GetComponent(cardEntityId);
			
			if(!walletCmp.HasEnoughCoins(shopSlotCmp.price))
				return;

			Debug.Log("F");
			
			walletCmp.currentAmountOfCoins -= shopSlotCmp.price;
			
			_shopSlotPool.DelComponent(cardEntityId);
			_updateCardsViewEventPool.AddComponent(_world.NewEntity());
		}
	}
}