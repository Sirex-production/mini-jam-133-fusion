using Secs;

namespace Ingame.Recipe
{
    public struct DiscoverNewRecipeEvent : IEcsComponent
    {
        public Recipe newRecipe;
    }
}