using Secs;
using UnityEngine;

namespace Ingame
{
	public sealed class UpdateCardMotionSystem : IEcsRunSystem
	{
		[EcsInject(typeof(CardCmp), typeof(TransformMdl))]
		private readonly EcsFilter _cardsFilter;
		
		[EcsInject]
		private readonly EcsPool<CardCmp> _cardPool;
		[EcsInject]
		private readonly EcsPool<IsInRestStateCardTag> _isInRestStateCardTagPool;
		
		[EcsInject]
		private readonly EcsPool<IsFollowingMouseTag> _isFollowingMouseTagPool;
		[EcsInject]
		private readonly EcsPool<IsUnderDOTweenAnimationTag> _isUnderDOTweenAnimationTagPool;
		

		private readonly GeneralCardsConfig _cardsConfig;
		
		public UpdateCardMotionSystem(GeneralCardsConfig cardsConfig)
		{
			_cardsConfig = cardsConfig;
		}
		
		public void OnRun()
		{
			foreach(var entity in _cardsFilter)
			{
				ref var cardCmp = ref _cardPool.GetComponent(entity);
				
				cardCmp.timePassedSinceLastDrop += Time.deltaTime;
				
				if(_isFollowingMouseTagPool.HasComponent(entity) || _isUnderDOTweenAnimationTagPool.HasComponent(entity)) 
					cardCmp.timePassedSinceLastDrop = 0f;
				
				if(cardCmp.timePassedSinceLastDrop >= _cardsConfig.TimeShouldPassToBeInRestState && !_isInRestStateCardTagPool.HasComponent(entity))
					_isInRestStateCardTagPool.AddComponent(entity);
				
				if(cardCmp.timePassedSinceLastDrop < _cardsConfig.TimeShouldPassToBeInRestState && _isInRestStateCardTagPool.HasComponent(entity))
					_isInRestStateCardTagPool.DelComponent(entity);
			}
		}
	}
}