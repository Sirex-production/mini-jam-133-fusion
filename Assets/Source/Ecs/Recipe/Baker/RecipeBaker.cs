using System.Collections.Generic;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Recipe
{
    public sealed class RecipeBaker : MonoBehaviour
    {
        [SerializeField] private List<ItemConfig> startingItems;
        private EcsWorld _ecsWorld;

        [Inject]
        private void Construct(EcsWorldsProvider ecsWorldsProvider)
        {
            _ecsWorld = ecsWorldsProvider.GameplayWorld;
        }
        
        private void Awake()
        {
            var entity = _ecsWorld.NewEntity();
            _ecsWorld.GetPool<StartingItemsMdl>().AddComponent(entity).startingItems = startingItems;
            _ecsWorld.GetPool<RecipeStatusMdl>().AddComponent(entity);
            
            _ecsWorld.UpdateFilters();
            
            gameObject.LinkEcsEntity(_ecsWorld,entity);
        }
    }
}