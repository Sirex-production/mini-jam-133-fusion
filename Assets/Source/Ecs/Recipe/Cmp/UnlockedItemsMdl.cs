﻿using System.Collections.Generic;
using Secs;

namespace Ingame.Recipe
{
    public struct UnlockedItemsMdl : IEcsComponent
    {
        public HashSet<ItemConfig> items;
    }
}