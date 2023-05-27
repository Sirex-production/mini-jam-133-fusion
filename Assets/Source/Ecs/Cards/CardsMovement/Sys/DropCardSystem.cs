using Secs;

namespace Ingame
{
	public sealed class DropCardSystem : IEcsRunSystem
	{
		[EcsInject(typeof(RigidbodyMdl), typeof(IsCardTag), typeof(IsFollowingMouseTag))]
		private readonly EcsFilter _isFollowingMouseCardFilter;
		
		[EcsInject]
		private readonly EcsPool<IsFollowingMouseTag> _isFollowingMouseTagPool;
		[EcsInject]
		private readonly EcsPool<RigidbodyMdl> _rigidbodyPool;

		[EcsInject]
		private readonly EcsWorld _world;
		
		private readonly InputService _inputService;

		public DropCardSystem(InputService inputService)
		{
			_inputService = inputService;
		}
		
		public void OnRun()
		{
			if(_isFollowingMouseCardFilter.IsEmpty)
				return;
			
			if(_inputService.IsLeftMousePressed)
				return;
			
			int cardEntityId = _isFollowingMouseCardFilter.GetFirstEntity();
			
			ref var rigidbodyMdl = ref _rigidbodyPool.GetComponent(cardEntityId);
			
			rigidbodyMdl.rigidbody.isKinematic = false;
			
			_isFollowingMouseTagPool.DelComponent(cardEntityId);
		}
	}
}