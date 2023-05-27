using Secs;
using UnityEngine;

namespace Ingame
{
	public sealed class SelectCardSystem : IEcsRunSystem
	{
		[EcsInject(typeof(CameraMdl), typeof(MainCameraTag))]
		private readonly EcsFilter _cameraFilter;
		[EcsInject(typeof(IsFollowingMouseTag))]
		private readonly EcsFilter _isFollowingMouseTagFilter;
		
		[EcsInject]
		private readonly EcsPool<CameraMdl> _cameraPool;
		[EcsInject]
		private readonly EcsPool<IsFollowingMouseTag> _isFollowingMouseTagPool;
		[EcsInject]
		private readonly EcsPool<CardCmp> _isCardTagPool;
		[EcsInject]
		private readonly EcsPool<RigidbodyMdl> _rigidbodyPool;
		
		private readonly InputService _inputService;

		public SelectCardSystem(InputService inputService)
		{
			_inputService = inputService;
		}
		
		public void OnRun()
		{
			if(_cameraFilter.IsEmpty || !_isFollowingMouseTagFilter.IsEmpty)
				return;

			if(!_inputService.IsLeftMouseClicked)
				return;

			var mousePosition = _inputService.MousePosition;
			ref var cameraMdl = ref _cameraPool.GetComponent(_cameraFilter.GetFirstEntity());

			var ray = cameraMdl.camera.ScreenPointToRay(mousePosition);

			if(!Physics.Raycast(ray, out RaycastHit hit, 100f))
				return;

			if(!hit.collider.TryGetComponent(out EcsEntityReference entityReference))
				return;

			if(!_isCardTagPool.HasComponent(entityReference.EntityId))
				return;

			ref var rigidbodyMdl = ref _rigidbodyPool.GetComponent(entityReference.EntityId);

			rigidbodyMdl.rigidbody.isKinematic = true;
			_isFollowingMouseTagPool.AddComponent(entityReference.EntityId);
		}
	}
}