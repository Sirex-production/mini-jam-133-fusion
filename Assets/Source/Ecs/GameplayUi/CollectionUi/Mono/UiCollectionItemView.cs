using Ingame.Recipe;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Ingame
{
	public sealed class UiCollectionItemView : MonoBehaviour
	{
		[Required, SerializeField] private Image itemIconImage;
		[Required, SerializeField] private Transform undiscoveredViewTransform;
		
		public void SetItemView(ItemConfig itemConfig, bool isDiscovered)
		{
			itemIconImage.sprite = itemConfig.ItemIcon;
			undiscoveredViewTransform.gameObject.SetActive(isDiscovered);
		}
	}
}