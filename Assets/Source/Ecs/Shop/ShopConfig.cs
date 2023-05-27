using System;
using System.Collections.Generic;
using System.Linq;
using Ingame.Recipe;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ingame
{
	[CreateAssetMenu(menuName = "Ingame/ShopConfig")]
	public sealed class ShopConfig : ScriptableObject
	{
		[SerializeField] [Min(0)] private int refreshCost;
		
		[SerializeField] [Min(0)] private int initialAmountOfCoins;
		[SerializeField] private ShopItemData[] shopItems;
		
		private Dictionary<ItemConfig, ShopItemData> _cachedShopItems;

		public int RefreshCost => refreshCost;
		public int InitialAmountOfCoins => initialAmountOfCoins;

		public bool TryGetShopItemData(ItemConfig itemConfig, out ShopItemData shopItemData)
		{
			_cachedShopItems ??= shopItems.ToDictionary(x => x.itemConfig, x => x);

			if(!_cachedShopItems.ContainsKey(itemConfig))
			{
				shopItemData = default;
				return false;
			}

			shopItemData = _cachedShopItems[itemConfig];
			return true;
		}
		
		public IEnumerable<ShopItemData> GetRandomItems(int itemCount)
		{
			for (int i = 0; i < itemCount; i++)
				yield return shopItems[Random.Range(0, shopItems.Length)];
		}

		[Serializable]
		public struct ShopItemData
		{
			public ItemConfig itemConfig;
			public int price;
		}
	}
}