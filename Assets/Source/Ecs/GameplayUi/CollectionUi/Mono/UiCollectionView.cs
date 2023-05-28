using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Ingame.Recipe;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ingame
{
	public sealed class UiCollectionView : MonoBehaviour
	{
		[BoxGroup("References")]
		[Required, SerializeField] private UiCollectionItemView collectionItemViewPrefab;
		[BoxGroup("References")]
		[Required, SerializeField] private Transform itemParentTransform;
		[BoxGroup("References")]
		[Required, SerializeField] private Button closeButton;
		
		[BoxGroup("References (Animation)")]
		[Required, SerializeField] private CanvasGroup backgroundCanvasGroup;
		[BoxGroup("References (Animation)")]
		[Required, SerializeField] private Transform frameTransform;
		
		[BoxGroup("Animation")]
		[SerializeField] [Min(0f)] private float animationDuration = .3f;

		private InputService _inputService;
		private SoundService _soundService;
		
		private HashSet<ItemConfig> _allItemsSet;
		private List<UiCollectionItemView> _currentCollectionItemViews = new();
		
		public event Action OnCollectionShown; 
		public event Action OnCollectionClosed;

		[Inject]
		private void Construct(InputService inputService, AllItemsConfig allItemsConfig, SoundService soundService)
		{
			_inputService = inputService;
			_soundService = soundService;
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
			_soundService.PlayUiClickSound();
			Hide();
		}

		private void Hide()
		{
			_inputService.MovementEnabled = true;
			
			OnCollectionClosed?.Invoke();
			
			backgroundCanvasGroup.DOFade(0f, animationDuration);
			frameTransform.DOScale(0f, animationDuration)
				.OnComplete(() => gameObject.SetActive(false));
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
			
			backgroundCanvasGroup.DOFade(1f, animationDuration);
			frameTransform.DOScale(1f, animationDuration);
		}
	}
}