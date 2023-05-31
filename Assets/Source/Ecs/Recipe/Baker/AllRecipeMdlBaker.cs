using Secs;
using UnityEngine;

namespace Ingame.Recipe
{
    public sealed class AllRecipeMdlBaker : EcsMonoBaker
    {
        [SerializeField] private AllRecipeContainerConfig allRecipeContainerConfig;

        protected override void Bake(EcsWorld world, int entityId)
        {
            world.GetPool<AllRecipesMdl>().AddComponent(entityId).AllRecipeContainerConfig = allRecipeContainerConfig;
        }
    }
}