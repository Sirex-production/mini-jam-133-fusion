using Secs;

namespace Ingame.Recipe
{
    public struct FusionReq : IEcsComponent
    {
        public ItemConfig firstItem;
        public ItemConfig secondItem;
    }
}