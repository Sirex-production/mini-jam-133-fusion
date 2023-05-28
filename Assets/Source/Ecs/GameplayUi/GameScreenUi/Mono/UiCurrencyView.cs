using System;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ingame
{
	public sealed class UiCurrencyView : MonoBehaviour
	{
		[BoxGroup("References")]
		[Required, SerializeField] private UiCollectionView collectionView;
		[BoxGroup("References")]
		[Required, SerializeField] private TMP_Text currencyText;
		
		[BoxGroup("Animation")]
		[SerializeField] private float animationDuration;

		private void Awake()
		{
			collectionView.OnCollectionShown += Hide;
			collectionView.OnCollectionClosed += Show;
		}

		private void OnDestroy()
		{
			collectionView.OnCollectionShown += Hide;
			collectionView.OnCollectionClosed += Show;
		}

		private void Show()
		{
			gameObject.SetActive(true);

			transform
				.DOScale(1f, animationDuration);
		}

		private void Hide()
		{
			transform
				.DOScale(0f, animationDuration)
				.OnComplete(() => gameObject.SetActive(false));
		}
		
		public void SetCurrency(int currency)
		{
			currency = Mathf.Max(0, currency);
			currencyText.SetText(currency.ToString());
		}
	}
}