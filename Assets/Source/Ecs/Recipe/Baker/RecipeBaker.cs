using System.Collections.Generic;
using Secs;
using UnityEngine;

namespace Ingame.Recipe
{
    public sealed class RecipeBaker : EcsMonoBaker
    {
        [SerializeField] private List<ItemConfig> startingItems;
        
        protected override void Bake(EcsWorld world, int entityId)
        {
            world.GetPool<StartingItemsMdl>().AddComponent(entityId).startingItems = startingItems;
            world.GetPool<RecipeStatusMdl>().AddComponent(entityId);
            world.GetPool<UnlockedItemsMdl>().AddComponent(entityId);
        }
    }
}