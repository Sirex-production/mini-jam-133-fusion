using System.Collections.Generic;
using Secs;

namespace Ingame.Recipe
{
    public struct StartingItemsMdl : IEcsComponent
    {
        public List<ItemConfig> startingItems;
    }
}