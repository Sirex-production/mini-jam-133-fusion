using Secs;

namespace Ingame.Recipe
{
    public sealed class TryToCombineSys : IEcsRunSystem
    {
        [EcsInject] private readonly EcsWorld _ecsWord;
        
        [EcsInject(typeof(FusionReq))]
        private readonly EcsFilter _fusionReqFilter;
        
        [EcsInject(typeof(RecipeStatusMdl))]
        private readonly EcsFilter _recipeStatusMdlFilter;
        
        [EcsInject( typeof(AllRecipesMdl))]
        private readonly EcsFilter _allReceiptsFilter;
        
        [EcsInject(typeof(UnlockedItemsMdl))]
        private readonly EcsFilter _unlockedItemsFilter;
        
        
        [EcsInject]
        private readonly EcsPool<RecipeStatusMdl> _recipeStatusMdlPool;
        
        [EcsInject]
        private readonly EcsPool<FusionReq> _fusionReqPool;

        [EcsInject]
        private readonly EcsPool<UnlockedItemsMdl> _unlockedItemsPool;

        [EcsInject] 
        private readonly EcsPool<AllRecipesMdl> _allRecipesMdlPool;
        
        public void OnRun()
        {
            foreach (var fusionEntity in _fusionReqFilter)
            {
                //todo check if formula is valid + check if it's a new
                _ecsWord.DelEntity(fusionEntity);
            }
        }
    }
}