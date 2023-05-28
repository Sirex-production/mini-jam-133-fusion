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
		private HashSet<ItemConfig> _allItemsSet;
		
		private List<UiCollectionItemView> _currentCollectionItemViews = new();
		
		public event Action OnCollectionShown; 
		public event Action OnCollectionClosed;

		[Inject]
		private void Construct(InputService inputService, AllItemsConfig allItemsConfig)
		{
			_inputService = inputService;
			_allItemsSet = allItemsConfig.AllItems.ToHashSet();
		}
		
		private void Awake()
		{
			closeButton.onClick.AddListener(OnCloseButtonClicked);
			Hide();

			for(int i = 0; i < _allItemsSet.Count; i++)
			{
				var collectionItemView = Instantiate(collectionItemViewPrefab, itemParentTransform);
				_currentCollectionItemViews.Add(collectionItemView);
			}
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
			int currentItemViewIndex = 0;
			
			foreach(var itemConfig in _allItemsSet)
			{
				bool isItemUnlocked = !unlockedItems.Contains(itemConfig);
				_currentCollectionItemViews[currentItemViewIndex].SetItemView(itemConfig, isItemUnlocked);

				currentItemViewIndex++;
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