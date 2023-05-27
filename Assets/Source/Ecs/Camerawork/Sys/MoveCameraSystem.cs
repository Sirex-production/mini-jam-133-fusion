using Secs;
using UnityEngine;

namespace Ingame
{
	public sealed class MoveCameraSystem : IEcsRunSystem
	{
		[EcsInject(typeof(TransformMdl), typeof(VirtualCameraMdl), typeof(CameraMovementBoundsCmp), typeof(MainVirtualCameraTag))]
		private readonly EcsFilter _mainVirtualCameraFilter;
		
		[EcsInject]
		private readonly EcsPool<VirtualCameraMdl> _virtualCameraPool;
		[EcsInject]
		private readonly EcsPool<TransformMdl> _transformPool;
		[EcsInject]
		private readonly EcsPool<CameraMovementBoundsCmp> _cameraMovementBoundsPool;

		private readonly InputService _inputService;
		private readonly SettingsService _settingsService;

		public MoveCameraSystem(InputService inputService, SettingsService settingsService)
		{
			_inputService = inputService;
			_settingsService = settingsService;
		}
		
		public void OnRun()
		{
			if(_mainVirtualCameraFilter.IsEmpty)
				return;

			int mainVirtualCameraEntity = _mainVirtualCameraFilter.GetFirstEntity();
			ref var transformMdl = ref _transformPool.GetComponent(mainVirtualCameraEntity);
			ref var cameraBoundsCmp = ref _cameraMovementBoundsPool.GetComponent(mainVirtualCameraEntity);
			ref var currentSettings = ref _settingsService.currentSettingsData;

			var minCameraBounds = transformMdl.initialLocalPos - cameraBoundsCmp.movementBounds;
			var maxCameraBounds = transformMdl.initialLocalPos + cameraBoundsCmp.movementBounds;
			Vector2 cameraMovementInput;
			
			if(_inputService.IsMiddleMousePressed || _inputService.IsRightMousePressed)
				cameraMovementInput = -_inputService.MouseDelta * currentSettings.mapMouseDraggingSpeed;
			else
				cameraMovementInput = _inputService.CameraMovement;
			
			var movementOffset = new Vector3
			{
				x = -cameraMovementInput.y * (currentSettings.cameraMovementSpeed * Time.deltaTime),
				z = cameraMovementInput.x * (currentSettings.cameraMovementSpeed * Time.deltaTime)
			};
			
			var newCameraPos = transformMdl.transform.position + movementOffset;
			
			newCameraPos.x = Mathf.Clamp(newCameraPos.x, minCameraBounds.x, maxCameraBounds.x);
			newCameraPos.z = Mathf.Clamp(newCameraPos.z, minCameraBounds.z, maxCameraBounds.z);

			transformMdl.transform.position = newCameraPos;
		}
	}
}