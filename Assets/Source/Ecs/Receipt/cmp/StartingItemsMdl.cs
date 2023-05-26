using System.Collections.Generic;
using Secs;

namespace Ingame.Receipt
{
    public struct StartingItemsMdl : IEcsComponent
    {
        public List<ItemConfig> startingItems;
    }
}