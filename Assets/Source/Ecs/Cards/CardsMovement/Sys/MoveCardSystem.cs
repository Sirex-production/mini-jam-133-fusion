using System.Runtime.CompilerServices;
using Secs;
using UnityEngine;

namespace Ingame
{
	public sealed class MoveCardSystem : IEcsRunSystem
	{
		[EcsInject(typeof(CardCmp), typeof(IsFollowingMouseTag), typeof(TransformMdl))]
		private readonly EcsFilter _followingMouseCardFilter;
		
		[EcsInject(typeof(CameraMdl), typeof(MainCameraTag))]
		private readonly EcsFilter _cameraFilter;
		
		[EcsInject(typeof(TransformMdl), typeof(GameSurfaceTag))]
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
			var cameraPos = mainCameraMdl.camera.transform.position;

			if(!GetCraftingSurfacePosition(mousePosition, mainCameraMdl.camera, out Vector3 hitPos))
				return;
			
			foreach(var entity in _followingMouseCardFilter)
			{
				//Move card
				ref var cardTransformMdl = ref _transformPool.GetComponent(entity);
				var cardTransform = cardTransformMdl.transform;
				var directionTowardsCamera = Vector3.Normalize(cameraPos - cardTransform.position);
				var targetCardPos = hitPos + directionTowardsCamera * _cardsConfig.OffsetFromCraftingSurfaceWhenDragging;

				cardTransform.position = Vector3.Lerp
				(
					cardTransform.position,
					targetCardPos,
					1f - Mathf.Pow(_cardsConfig.CardsFollowCursorDumping, Time.deltaTime)
				);
				
				//Rotate card
				var movingDirection = Vector3.Normalize(targetCardPos - cardTransform.position);
				var targetCardEulerAngles = cardTransformMdl.initialLocalRot.eulerAngles;

				movingDirection *= _cardsConfig.CardsRotationStrength;
				movingDirection.x = Mathf.Clamp(movingDirection.x, -_cardsConfig.CardsRotationAngle, _cardsConfig.CardsRotationAngle);
				movingDirection.y = 0;
				movingDirection.z = Mathf.Clamp(movingDirection.z, -_cardsConfig.CardsRotationAngle, _cardsConfig.CardsRotationAngle);

				targetCardEulerAngles.x += -movingDirection.z;
				targetCardEulerAngles.z += movingDirection.x;

				cardTransform.eulerAngles = targetCardEulerAngles;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool GetCraftingSurfacePosition(Vector2 mousePosition, Camera camera, out Vector3 hitPoint)
		{
			var ray = camera.ScreenPointToRay(mousePosition);
			int raycastMask = ~LayerMask.GetMask("Card");

			if(!Physics.Raycast(ray, out RaycastHit hit,100f,  raycastMask))
			{
				hitPoint = Vector3.zero;
				return false;
			}
			
			hitPoint = hit.point;
			return true;
		}
	}
}