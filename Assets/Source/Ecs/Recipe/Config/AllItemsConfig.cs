using UnityEngine;

namespace Ingame.Recipe
{
	[CreateAssetMenu(fileName = "AllItemsConfig", menuName = "Ingame/AllItemsConfig")]
	public sealed class AllItemsConfig : ScriptableObject
	{
		[SerializeField] private ItemConfig[] _allItems;
		
		public ItemConfig[] AllItems => _allItems;
	}
}