using DG.Tweening;
using Secs;
using UnityEngine;

namespace Ingame
{
	public sealed class MoveShopCardsBackToShopSystem : IEcsRunSystem
	{
		public const float JUMP_POWER = 10f;
		public const int JUMP_COUNT = 1;
		public const float JUMP_DURATION = 1f;
		
		[EcsInject(typeof(CardCmp), typeof(ShopSlotCmp), typeof(TransformMdl), typeof(RigidbodyMdl), typeof(IsInRestStateCardTag))]
		[AndExclude(typeof(IsFollowingMouseTag), typeof(IsUnderDOTweenAnimationTag))]
		private readonly EcsFilter _shopCardsFilter;
		
		[EcsInject(typeof(ShopCmp))]
		private readonly EcsFilter _shopFilter;
		
		[EcsInject]
		private EcsPool<TransformMdl> _transformPool;
		[EcsInject]
		private EcsPool<ShopSlotCmp> _shopSlotPool;
		[EcsInject]
		private EcsPool<RigidbodyMdl> _rigidbodyPool;
		[EcsInject]
		private EcsPool<IsUnderDOTweenAnimationTag> _isUnderDOTweenAnimationTagPool;

		[EcsInject]
		private readonly EcsPool<ShopCmp> _shopPool;

		public void OnRun()
		{
			if(_shopCardsFilter.IsEmpty)
				return;
			
			ref var shopCmp = ref _shopPool.GetComponent(_shopFilter.GetFirstEntity());

			foreach(var entity in _shopCardsFilter)
			{
				ref var transformMdl = ref _transformPool.GetComponent(entity);
				ref var shopSlotCmp = ref _shopSlotPool.GetComponent(entity);
				ref var rigidbodyMdl = ref _rigidbodyPool.GetComponent(entity);

				var cardTransform = transformMdl.transform;
				var cardRigidbody = rigidbodyMdl.rigidbody;
				var targetTransform = shopCmp.slotsPositions[shopSlotCmp.slotIndex];

				if(Vector3.Distance(cardTransform.position, targetTransform.position) < 5f)
					continue;

				_isUnderDOTweenAnimationTagPool.AddComponent(entity);

				cardRigidbody.isKinematic = true;
				cardTransform.DOKill();

				cardTransform
					.DOJump(targetTransform.position, JUMP_POWER, JUMP_COUNT, JUMP_DURATION)
					.SetEase(Ease.OutQuad)
					.SetLink(cardTransform.gameObject)
					.OnComplete
					(
						() =>
						{
							cardRigidbody.isKinematic = false;
							_isUnderDOTweenAnimationTagPool.DelComponent(entity);
						}
					);
			}
		}
	}
}