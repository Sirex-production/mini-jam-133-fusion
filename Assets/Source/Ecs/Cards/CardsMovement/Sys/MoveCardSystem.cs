using Secs;
using UnityEngine;

namespace Ingame
{
	public sealed class MoveCardSystem : IEcsRunSystem
	{
		[EcsInject(typeof(IsCardTag), typeof(IsFollowingMouseTag), typeof(TransformMdl))]
		private readonly EcsFilter _followingMouseCardFilter;
		
		[EcsInject(typeof(CameraMdl), typeof(MainCameraTag))]
		private readonly EcsFilter _cameraFilter;
		
		[EcsInject(typeof(TransformMdl), typeof(CraftingSurfaceTag))]
		private readonly EcsFilter _craftingSurfaceFilter;
		
		[EcsInject]
		private readonly EcsPool<TransformMdl> _transformPool;
		
		[EcsInject]
		private readonly EcsPool<CameraMdl> _cameraPool;

		private readonly InputService _inputService;
		private readonly GeneralCardsConfig _cardsConfig;

		public MoveCardSystem(InputService inputService, GeneralCardsConfig cardsConfig)
		{
			_inputService = inputService;
			_cardsConfig = cardsConfig;
		}
		
		public void OnRun()
		{
			var mousePosition = _inputService.MousePosition;
			
			if(_cameraFilter.IsEmpty || _craftingSurfaceFilter.IsEmpty)
				return;
			
			ref var mainCameraMdl = ref _cameraPool.GetComponent(_cameraFilter.GetFirstEntity());
			var craftingSurfacePos = _transformPool.GetComponent(_craftingSurfaceFilter.GetFirstEntity()).transform.position;

			foreach(var entity in _followingMouseCardFilter)
			{
				var cardTransform = _transformPool.GetComponent(entity).transform;
				var cursorPositionInWorldSpace = mainCameraMdl.camera.ScreenToWorldPoint(mousePosition);
				cursorPositionInWorldSpace.y = craftingSurfacePos.y;
				
				var targetCardPos = cursorPositionInWorldSpace + _cardsConfig.OffsetFromCraftingSurfaceWhenDragging;
				
				cardTransform.position = Vector3.Lerp
				(
					cardTransform.position,
					targetCardPos,
					1f - Mathf.Pow(_cardsConfig.CardsFollowCursorDumping, Time.deltaTime)
				);
			}
		}
	}
}