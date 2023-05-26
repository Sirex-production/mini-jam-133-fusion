using Ingame.Cmp;
using Secs;
using UnityEngine;

namespace Ingame.Sys
{
	public sealed class MoveCardSystem : IEcsRunSystem
	{
		[EcsInject(typeof(CardViewMdl), typeof(MainCameraTag))]
		private readonly EcsFilter _cameraFilter;
		[EcsInject(typeof(TransformMdl), typeof(CraftingSurfaceTag))]
		private readonly EcsFilter _craftingSurfaceFilter;
		[EcsInject]
		private readonly EcsPool<CameraMdl> _cameraPool;
		[EcsInject]
		private readonly EcsPool<TransformMdl> _transformMdlPool;
		
		private readonly InputService _inputService;
		private readonly GeneralCardsConfig _generalCardsConfig;

		public MoveCardSystem(InputService inputService, GeneralCardsConfig generalCardsConfig)
		{
			_inputService = inputService;
			_generalCardsConfig = generalCardsConfig;
		}
		
		public void OnRun()
		{
			if(_cameraFilter.IsEmpty || _craftingSurfaceFilter.IsEmpty)
				return;
			
			if(!_inputService.IsLeftMousePressed)
				return;
			
			var mousePosition = _inputService.MousePosition;
			var craftingSurfaceTransform = _transformMdlPool.GetComponent(_craftingSurfaceFilter.GetFirstEntity()).transform;
			ref var cameraMdl = ref _cameraPool.GetComponent(_cameraFilter.GetFirstEntity());

			var ray = cameraMdl.camera.ScreenPointToRay(mousePosition);

			if(!Physics.Raycast(ray, out RaycastHit hit, 100f))
				return;

			if(!hit.collider.TryGetComponent(out CardView cardView))
				return;
			
			// if(cardView.TryGetComponent<>())
		}
	}
}