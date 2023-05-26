using Secs;

namespace Ingame.Recipe
{
    public struct DiscoverNewRecipeReq : IEcsComponent
    {
        public Recipe newRecipe;
    }
}