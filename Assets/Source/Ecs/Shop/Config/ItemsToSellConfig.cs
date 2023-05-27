 

using System;
using System.Collections.Generic;
using Ingame.Recipe;
using UnityEngine;

namespace Ingame.Shop
{
    [CreateAssetMenu(fileName = "ItemsToSellConfig", menuName = "Shop/ItemsToSellConfig")]
    public sealed class ItemsToSellConfig : ScriptableObject
    {
        [SerializeField] 
        private List<ItemInShop> itemsToSell;

        public List<ItemInShop> ItemsToSell => itemsToSell;
    }

    [Serializable]
    public sealed class ItemInShop
    {
        [SerializeField]
        private float cost;

        [SerializeField] 
        private ItemConfig item;

        public float Cost => cost;

        public ItemConfig Item => item;
    }
}