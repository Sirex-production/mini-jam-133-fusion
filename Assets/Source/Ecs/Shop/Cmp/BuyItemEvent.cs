using Secs;

namespace Ingame.Shop
{
    public struct BuyItemEvent : IEcsComponent
    {
        public ItemInShop requestedItem;
    }
}