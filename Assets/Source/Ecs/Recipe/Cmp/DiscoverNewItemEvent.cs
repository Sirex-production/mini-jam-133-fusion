using Secs;

namespace Ingame.Recipe
{
    public struct DiscoverNewItemEvent : IEcsComponent
    {
        public ItemConfig item;
    }
}