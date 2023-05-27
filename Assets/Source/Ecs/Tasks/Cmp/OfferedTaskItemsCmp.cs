
using System.Collections.Generic;
using System.Linq;
using Ingame.Recipe;
using Secs;
using UnityEngine;
namespace Ingame.Tasks
{
    public struct OfferedTaskItemsCmp : IEcsComponent
    {
        public Dictionary<ItemConfig, List<Transform>> offeredItems;

        public void Add(ItemConfig itemConfig, Transform transform)
        {
            offeredItems ??= new();
            if (!offeredItems.ContainsKey(itemConfig))
            {
                offeredItems.Add(itemConfig,new List<Transform>(){transform});
                return;
            }
            
            offeredItems[itemConfig].Add(transform);
        }

        public void Remove(ItemConfig itemConfig, Transform transform)
        {
            offeredItems ??= new();
            
            if (!offeredItems.ContainsKey(itemConfig))
            {
                return;
            }

            offeredItems[itemConfig].Remove(transform);
        }

        public bool IsTradeAccepted(List<ItemConfig> items)
        {
            
            return true;
        }

        public void SubtractItems(List<ItemConfig> items)
        {
            
        }
    }
 
}