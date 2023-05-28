
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
            
            if(offeredItems[itemConfig].Contains(transform))
                return;
            
            offeredItems[itemConfig].Add(transform);
        }

        public void Remove(ItemConfig itemConfig, Transform transform)
        {
            offeredItems ??= new();
            
            if (!offeredItems.ContainsKey(itemConfig))
                return;
            
            if(offeredItems[itemConfig].Contains(transform))
                offeredItems[itemConfig].Remove(transform);
        }

        public bool IsTradeAccepted(List<ItemConfig> items)
        {
            var hash = new HashSet<ItemConfig>(items);
            var dictionary = hash.ToDictionary(
                e => e, 
                e => items.Select(item => e==item).Count());

            foreach (var key in dictionary.Keys)
            {
                if (!offeredItems.ContainsKey(key))
                    return false;

                if (offeredItems[key].Count < dictionary[key])
                    return false;
            }

            return true;
        }

        public void SubtractItems(List<ItemConfig> items)
        {
            var hash = new HashSet<ItemConfig>(items);
            var dictionary = hash.ToDictionary(
                e => e, 
                e => items.Select(item => e==item).Count());
            
            foreach (var key in dictionary.Keys)
            {
                for (int i = 0; i < dictionary[key]; i++)
                {
                    var toRemoveObject = offeredItems[key][0];
                    var entityReference = toRemoveObject.GetComponent<EcsEntityReference>();

                    var newEntity = entityReference.World.NewEntity();
                    ref var entityDestroyer = ref entityReference.World.GetPool<FullDestroyObjectRequest>().AddComponent(newEntity);

                    entityDestroyer.entityId = entityReference.EntityId;
                    entityDestroyer.gameObject = toRemoveObject.gameObject;
                    
                    offeredItems[key].RemoveAt(0);
                }
            }
        }
    }
 
}