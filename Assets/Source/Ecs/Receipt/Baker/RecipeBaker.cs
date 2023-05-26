using System;
using System.Collections.Generic;
using Secs;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Ingame.Receipt
{
    public sealed class RecipeBaker : MonoBehaviour
    {
        [SerializeField] private List<ItemConfig> startingItems;
        [FormerlySerializedAs("allReceiptsContainerConfig")] [SerializeField] private AllRecipeContainerConfig allRecipeContainerConfig;
        private EcsWorld _world;

        [Inject]
        private void Construct(EcsWorldsProvider ecsWorldsProvider)
        {
            _world = ecsWorldsProvider.GameplayWorld;
        }
        
        private void Awake()
        {
            var entity = _world.NewEntity();
            _world.GetPool<AllRecipesMdl>().AddComponent(entity).AllRecipeContainerConfig = allRecipeContainerConfig;
            _world.GetPool<StartingItemsMdl>().AddComponent(entity).startingItems = startingItems;
            _world.GetPool<RecipeStatusMdl>().AddComponent(entity);
            
            gameObject.LinkEcsEntity(_world,entity);
        }
    }
}