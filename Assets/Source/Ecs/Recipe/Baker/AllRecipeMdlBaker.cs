using System.Collections.Generic;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Receipt
{
    public sealed class AllRecipeMdlBaker : MonoBehaviour
    {
        [SerializeField] private AllRecipeContainerConfig allRecipeContainerConfig;
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

            _world.UpdateFilters();
        }
    }
}