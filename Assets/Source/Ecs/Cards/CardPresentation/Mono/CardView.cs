using System;
using Ingame.Recipe;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ingame
{
	public sealed class CardView : MonoBehaviour
	{
		[BoxGroup("References (Icon)")]
		[SerializeField, Required] private Image cardIconImage;
		[BoxGroup("References (Icon)")]
		[SerializeField, Required] private Image cardBackgroundImage;
		
		[BoxGroup("References (Description)")]
		[SerializeField, Required] private Image cardDescriptionBackgroundImage;
		[BoxGroup("References (Description)")]
		[SerializeField, Required] private TMP_Text cardDescriptionText;
		
		[BoxGroup("References (Shop)")]
		[SerializeField, Required] private Transform shopInfoRootTransform;
		[BoxGroup("References (Shop)")]
		[SerializeField, Required] private Transform unavailableToBuyRootTransform;
		[BoxGroup("References (Shop)")]
		[SerializeField, Required] private TMP_Text priceText;

		public void UpdateCardView(ItemConfig itemConfig)
		{
			cardIconImage.sprite = itemConfig.ItemIcon;
			cardBackgroundImage.color = itemConfig.CardBackgroundColor;
			cardDescriptionBackgroundImage.color = itemConfig.CardDescriptionBackgroundColor;
			cardDescriptionText.SetText(itemConfig.ItemName);
		}

		public void TurnOnShopView(int price, bool isAvailableForSale)
		{
			unavailableToBuyRootTransform.gameObject.SetActive(!isAvailableForSale);
			priceText.SetText(price.ToString());
		}
		
		public void TurnOffShopView()
		{
			shopInfoRootTransform.gameObject.SetActive(false);
		}
	}
}