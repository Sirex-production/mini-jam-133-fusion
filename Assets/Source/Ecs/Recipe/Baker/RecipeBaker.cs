using System.Collections.Generic;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Receipt
{
    public sealed class RecipeBaker : MonoBehaviour
    {
        [SerializeField] private List<ItemConfig> startingItems;
        private EcsWorld _world;

        [Inject]
        private void Construct(EcsWorldsProvider ecsWorldsProvider)
        {
            _world = ecsWorldsProvider.GameplayWorld;
        }
        
        private void Awake()
        {
            var entity = _world.NewEntity();
            _world.GetPool<StartingItemsMdl>().AddComponent(entity).startingItems = startingItems;
            _world.GetPool<RecipeStatusMdl>().AddComponent(entity);
            
            _world.UpdateFilters();
            
            gameObject.LinkEcsEntity(_world,entity);
        }
    }
}