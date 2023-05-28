using Ingame.Recipe;
using Secs;
using UnityEngine;

namespace Ingame
{
	public sealed class UpdateUiCollectionViewSystem : IEcsRunSystem
	{
		[EcsInject(typeof(UiCollectionViewMdl))]
		private readonly EcsFilter _uiCollectionViewMdlFilter;
		[EcsInject(typeof(UnlockedItemsMdl))]
		private readonly EcsFilter _unlockedItemsMdlFilter;
		[EcsInject(typeof(UpdateCollectionsUiEvent))]
		private readonly EcsFilter _updateCollectionsUiEventFilter;
		
		[EcsInject]
		private readonly EcsPool<UiCollectionViewMdl> _uiCollectionViewMdlPool;
		[EcsInject]
		private readonly EcsPool<UnlockedItemsMdl> _unlockedItemsMdlPool;
		[EcsInject]
		private readonly EcsPool<UpdateCollectionsUiEvent> _updateCollectionsUiEventPool;

		public void OnRun()
		{
			if(_updateCollectionsUiEventFilter.IsEmpty)
				return;
			
			_updateCollectionsUiEventPool.DelComponent(_updateCollectionsUiEventFilter.GetFirstEntity());

			if(_uiCollectionViewMdlFilter.IsEmpty)
				return;

			ref var unlockedItemsMdl = ref _unlockedItemsMdlPool.GetComponent(_unlockedItemsMdlFilter.GetFirstEntity());

			foreach(var entity in _uiCollectionViewMdlFilter)
			{
				ref var uiCollectionViewMdl = ref _uiCollectionViewMdlPool.GetComponent(entity);
				uiCollectionViewMdl.uiCollectionView.UpdateCollectionItemsViews(unlockedItemsMdl.items);
			}
		}
	}
}