using Ingame.Recipe;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Ingame
{
	public sealed class UiCollectionItemView : MonoBehaviour
	{
		[Required, SerializeField] private Image itemIconImage;

		public void SetItemView(ItemConfig itemConfig)
		{
			itemIconImage.sprite = itemConfig.ItemIcon;
		}
	}
}