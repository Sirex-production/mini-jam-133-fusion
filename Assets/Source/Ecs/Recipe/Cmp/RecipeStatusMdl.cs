using System.Collections.Generic;
using Secs;

namespace Ingame.Recipe
{
    public struct RecipeStatusMdl : IEcsComponent
    {
        public HashSet<Recipe> discoveredRecipe;
        public HashSet<Recipe> unlockedRecipe;
    }
}