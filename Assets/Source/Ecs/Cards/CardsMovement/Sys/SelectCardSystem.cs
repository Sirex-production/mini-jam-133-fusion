using Secs;
using UnityEngine;

namespace Ingame
{
	public sealed class SelectCardSystem : IEcsRunSystem
	{
		[EcsInject(typeof(CameraMdl), typeof(MainCameraTag))]
		private readonly EcsFilter _cameraFilter;
		[EcsInject(typeof(IsFollowingMouseTag))]
		[AndExclude(typeof(IsUnderDOTweenAnimationTag))]
		private readonly EcsFilter _isFollowingMouseTagFilter;
		[EcsInject(typeof(PlayerWalletCmp))]
		private readonly EcsFilter _playerWalletFilter;
		
		[EcsInject]
		private readonly EcsPool<CameraMdl> _cameraPool;
		[EcsInject]
		private readonly EcsPool<IsFollowingMouseTag> _isFollowingMouseTagPool;
		[EcsInject]
		private readonly EcsPool<CardCmp> _isCardTagPool;
		[EcsInject]
		private readonly EcsPool<RigidbodyMdl> _rigidbodyPool;
		[EcsInject]
		private readonly EcsPool<ShopSlotCmp> _shopSlotPool;
		[EcsInject]
		private readonly EcsPool<PlayerWalletCmp> _playerWalletPool;
		
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

			if(!_playerWalletFilter.IsEmpty && _shopSlotPool.HasComponent(entityReference.EntityId))
			{
				ref var shopSlotCmp = ref _shopSlotPool.GetComponent(entityReference.EntityId);
				ref var playerWalletCmp = ref _playerWalletPool.GetComponent(_playerWalletFilter.GetFirstEntity());
					
				if(!playerWalletCmp.HasEnoughCoins(shopSlotCmp.price))
					return;
			}

			ref var rigidbodyMdl = ref _rigidbodyPool.GetComponent(entityReference.EntityId);

			rigidbodyMdl.rigidbody.isKinematic = true;
			_isFollowingMouseTagPool.AddComponent(entityReference.EntityId);
		}
	}
}