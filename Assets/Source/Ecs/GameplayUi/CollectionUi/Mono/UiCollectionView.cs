using System;
using System.Collections.Generic;
using System.Linq;
using Ingame.Recipe;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ingame
{
	public sealed class UiCollectionView : MonoBehaviour
	{
		[Required, SerializeField] private UiCollectionItemView collectionItemViewPrefab;
		[Required, SerializeField] private Transform itemParentTransform;
		[Required, SerializeField] private Button closeButton;

		private InputService _inputService;
		
		private List<UiCollectionItemView> _currentCollectionItemViews = new();
		
		public event Action OnCollectionShown; 
		public event Action OnCollectionClosed;

		[Inject]
		private void Construct(InputService inputService)
		{
			_inputService = inputService;
		}
		
		private void Awake()
		{
			closeButton.onClick.AddListener(OnCloseButtonClicked);
			Hide();
		}

		private void OnDestroy()
		{
			closeButton.onClick.RemoveListener(OnCloseButtonClicked);
		}

		private void OnCloseButtonClicked()
		{
			Hide();
		}

		private void Hide()
		{
			_inputService.MovementEnabled = true;
			gameObject.SetActive(false);
			OnCollectionClosed?.Invoke();
		}

		public void UpdateCollectionItemsViews(HashSet<ItemConfig> unlockedItems)
		{
			int itemsToAdd = unlockedItems.Count - _currentCollectionItemViews.Count;
			var unlockedItemsArray = unlockedItems.ToArray();

			for(int i = 0; i < itemsToAdd; i++)
			{
				var collectionItemView = Instantiate(collectionItemViewPrefab, itemParentTransform);
				_currentCollectionItemViews.Add(collectionItemView);
			}

			for(int itemIndex = 0; itemIndex < _currentCollectionItemViews.Count; itemIndex++)
			{
				if(itemIndex >= unlockedItems.Count)
				{
					_currentCollectionItemViews[itemIndex].gameObject.SetActive(false);
					continue;
				}
				
				_currentCollectionItemViews[itemIndex].gameObject.SetActive(true);
				_currentCollectionItemViews[itemIndex].SetItemView(unlockedItemsArray[itemIndex]);
			}
		}

		public void Show()
		{
			_inputService.MovementEnabled = false;
			gameObject.SetActive(true);
			OnCollectionShown?.Invoke();
		}
	}
}