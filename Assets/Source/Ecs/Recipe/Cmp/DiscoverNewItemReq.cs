using Secs;

namespace Ingame.Recipe
{
    public struct DiscoverNewItemReq : IEcsComponent
    {
        public ItemConfig item;
    }
}