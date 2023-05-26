using Secs;

namespace Ingame.Receipt
{
    public struct DiscoverNewRecipeReq : IEcsComponent
    {
        public Recipe newRecipe;
    }
}