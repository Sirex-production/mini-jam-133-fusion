using System.Collections.Generic;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Recipe
{
    public sealed class AllRecipeMdlBaker : MonoBehaviour
    {
        [SerializeField] private AllRecipeContainerConfig allRecipeContainerConfig;
        private EcsWorld _ecsWorld;

        [Inject]
        private void Construct(EcsWorldsProvider ecsWorldsProvider)
        {
            _ecsWorld = ecsWorldsProvider.GameplayWorld;
        }
        
        private void Awake()
        {
            var entity = _ecsWorld.NewEntity();
            _ecsWorld.GetPool<AllRecipesMdl>().AddComponent(entity).AllRecipeContainerConfig = allRecipeContainerConfig;

            _ecsWorld.UpdateFilters();
        }
    }
}