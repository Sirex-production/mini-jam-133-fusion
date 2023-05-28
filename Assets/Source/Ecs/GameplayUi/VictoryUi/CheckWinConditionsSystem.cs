using Ingame.Recipe;
using Secs;
using Zenject;

namespace Ingame
{
	public sealed class CheckWinConditionsSystem : IEcsRunSystem
	{
		[EcsInject]
		private readonly EcsWorld _world;
		
		[EcsInject(typeof(UnlockedItemsMdl))]
		private readonly EcsFilter _unlockedItemsFilter;
		[EcsInject(typeof(UiVictoryViewMdl))]
		private readonly EcsFilter _uiVictoryViewFilter;
		[EcsInject(typeof(IsVictoryTag))]
		private readonly EcsFilter _isVictoryFilter;

		[EcsInject]
		private readonly EcsPool<UnlockedItemsMdl> _unlockedItemsPool;
		[EcsInject]
		private readonly EcsPool<UiVictoryViewMdl> _uiVictoryViewPool;
		
		private readonly AllItemsConfig _allItemsConfig;

		public CheckWinConditionsSystem(AllItemsConfig allItemsConfig)
		{
			_allItemsConfig = allItemsConfig;
		}
		
		public void OnRun()
		{
			if(!_isVictoryFilter.IsEmpty)
				return;
			
			if(_unlockedItemsFilter.IsEmpty || _uiVictoryViewFilter.IsEmpty)
				return;
			
			ref var unlockedItemsMdl = ref _unlockedItemsPool.GetComponent(_unlockedItemsFilter.GetFirstEntity());

			if(unlockedItemsMdl.items.Count != _allItemsConfig.AllItems.Length)
				return;
			
			ref var uiVictoryViewMdl = ref _uiVictoryViewPool.GetComponent(_uiVictoryViewFilter.GetFirstEntity());
			_world.GetPool<IsVictoryTag>().AddComponent(_world.NewEntity());
			
			uiVictoryViewMdl.uiVictoryView.Show();
		}
	}
}