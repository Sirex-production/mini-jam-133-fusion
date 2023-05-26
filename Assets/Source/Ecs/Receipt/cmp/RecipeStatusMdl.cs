using System.Collections.Generic;
using Secs;

namespace Ingame.Receipt
{
    public struct RecipeStatusMdl : IEcsComponent
    {
        public List<Recipe> discoveredRecipe;
        public List<Recipe> unlockedRecipe;
    }
}