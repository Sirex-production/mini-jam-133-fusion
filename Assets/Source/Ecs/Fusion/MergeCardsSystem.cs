using DG.Tweening;
using Ingame.Audio;
using Ingame.Cmp;
using Ingame.Recipe;
using Ingame.VfX;
using Secs;
using UnityEngine;

namespace Ingame
{
	public sealed class MergeCardsSystem : IEcsRunSystem
	{
		private const float MOVE_DURATION = .1f;
		private const float SHAKE_DURATION = 1f;
		private const float SHAKE_STRENGTH = 1f;
		private const int  VIBRATION = 50;
		private const float SCALE = .5f;
		private const float SCALE_DURATION = .5f;
				
		private const float RESULT_CARD_SCALE = 1.3f;
		private const float RESULT_SCALE_DURATION = .1f;

		private SoundService _soundService;
		
		[EcsInject]
		private readonly EcsWorld _world;
		
		[EcsInject(typeof(OnCollisionEnterEvent))]
		private readonly EcsFilter _collisionEnterEvents;

		[EcsInject(typeof(AudioCmp),typeof(FusionSoundTag))]
		private readonly EcsFilter _fusionSoundFilter;
		
		[EcsInject]
		private readonly EcsPool<OnCollisionEnterEvent> _collisionEnterEventPool;
		[EcsInject]
		private readonly EcsPool<CardCmp> _cardPool;
		[EcsInject]
		private readonly EcsPool<UpdateCardsViewEvent> _updateCardsViewEventPool;
		[EcsInject]
		private readonly EcsPool<ShopSlotCmp> _shopSlotPool;
		[EcsInject]
		private readonly EcsPool<IsUnderDOTweenAnimationTag> _isUnderDOTweenAnimationPool;
		[EcsInject]
		private readonly EcsPool<DiscoverNewItemEvent> _discoverNewItemEventPool;
		[EcsInject]
		private readonly EcsPool<AudioCmp> _audioCmpPool;
		[EcsInject]
		private readonly EcsPool<ParticleSystemMdl> _particleSystemPool;
		
		private readonly AllRecipeContainerConfig _receiptConfig;
		
		public MergeCardsSystem(AllRecipeContainerConfig receiptConfig, SoundService soundService)
		{
			_receiptConfig = receiptConfig;
			_soundService = soundService;
		}
		
		public void OnRun()
		{
			foreach(var eventEntity in _collisionEnterEvents)
			{
				ref var collisionEnterEvent = ref _collisionEnterEventPool.GetComponent(eventEntity);
				
				if(collisionEnterEvent.collider == null || collisionEnterEvent.senderObject == null)
					continue;
				
				if(!collisionEnterEvent.senderObject.TryGetComponent(out EcsEntityReference senderEntityReference))
					continue;

				if(!collisionEnterEvent.collider.TryGetComponent(out EcsEntityReference otherEntityReference))
					continue;
				
				if(_world.IsEntityDead(senderEntityReference.EntityId) || _world.IsEntityDead(otherEntityReference.EntityId))
					continue;

				if(!_cardPool.HasComponent(senderEntityReference.EntityId) || !_cardPool.HasComponent(otherEntityReference.EntityId))
					continue;

				if(_isUnderDOTweenAnimationPool.HasComponent(senderEntityReference.EntityId) || _isUnderDOTweenAnimationPool.HasComponent(otherEntityReference.EntityId))
					continue;

				if(_shopSlotPool.HasComponent(senderEntityReference.EntityId) || _shopSlotPool.HasComponent(otherEntityReference.EntityId))
					continue;

				ref var senderCard = ref _cardPool.GetComponent(senderEntityReference.EntityId);
				ref var otherCard = ref _cardPool.GetComponent(otherEntityReference.EntityId);
				
				if(!_receiptConfig.TryToCombineElements(_world, senderCard.itemConfig, otherCard.itemConfig, out var resultItemConfig))
					continue;

				var senderCardTransform = senderEntityReference.transform;
				var otherCardTransform = otherEntityReference.transform;
				
				_isUnderDOTweenAnimationPool.AddComponent(senderEntityReference.EntityId);
				_isUnderDOTweenAnimationPool.AddComponent(otherEntityReference.EntityId);

				AudioSource audioSource = null;

				if(!_fusionSoundFilter.IsEmpty)
					audioSource = _soundService.PlaySound(_audioCmpPool.GetComponent(_fusionSoundFilter.GetFirstEntity()).audioClip);
				
				
				DOTween.Sequence()
					.Append(otherCardTransform.DOMove(senderCardTransform.position, MOVE_DURATION).SetLink(otherCardTransform.gameObject))
					.Join(senderCardTransform.DOShakePosition(SHAKE_DURATION, SHAKE_STRENGTH, VIBRATION, fadeOut: false).SetLink(senderCardTransform.gameObject))
					.Join(otherCardTransform.DOShakePosition(SHAKE_DURATION, SHAKE_STRENGTH, VIBRATION, fadeOut: false).SetLink(otherCardTransform.gameObject))
					.Join(senderCardTransform.DOScale(Vector3.one * SCALE, SCALE_DURATION).SetLink(senderCardTransform.gameObject))
					.Join
					(
						otherCardTransform.DOScale(Vector3.one * SCALE, SCALE_DURATION)
							.SetLink(otherCardTransform.gameObject)
							.OnComplete(() =>
							{
								_cardPool.GetComponent(senderEntityReference.EntityId).itemConfig = resultItemConfig;

								_updateCardsViewEventPool.AddComponent(_world.NewEntity());
								_discoverNewItemEventPool.AddComponent(_world.NewEntity()).item = resultItemConfig;
								otherEntityReference.gameObject.SetActive(false);

								var entity = senderEntityReference.EntityId;
								if (!_particleSystemPool.HasComponent(entity)) 
									return;
								
								var particle = _particleSystemPool.GetComponent(entity).particleSystem;
								
								particle.Clear();
								particle.Play();
							})
					)
					.Append(senderCardTransform.DOScale(Vector3.one * RESULT_CARD_SCALE, RESULT_SCALE_DURATION).SetLink(senderCardTransform.gameObject))
					.Append(senderCardTransform.DOScale(Vector3.one, RESULT_SCALE_DURATION).SetLink(senderCardTransform.gameObject))
					.OnComplete(() =>
					{
						if(audioSource!=null)
							_soundService.StopSound(audioSource);
						
						_isUnderDOTweenAnimationPool.DelComponent(senderEntityReference.EntityId);
						_world.DelEntity(otherEntityReference.EntityId);
						Object.Destroy(otherEntityReference.gameObject);
						
						var entity = senderEntityReference.EntityId;
						
					});
			}
		}
	}
}