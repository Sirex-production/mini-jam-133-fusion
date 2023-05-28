using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ingame
{
	public sealed class UiCollectionsButtonView : MonoBehaviour
	{
		[BoxGroup("References")]
		[Required, SerializeField] private UiCollectionView collectionView;
		[BoxGroup("References")]
		[Required, SerializeField] private Button collectionsButton;
		
		[BoxGroup("Animation")]
		[SerializeField] [Min(0f)] private float animationDuration = .2f;

		private SoundService _soundService;
		
		[Inject]
		private void Construct(SoundService soundService)
		{
			_soundService = soundService;
		}
		
		private void Awake()
		{
			collectionsButton.onClick.AddListener(OnCollectionsButtonClicked);
			collectionView.OnCollectionShown += Hide;
			collectionView.OnCollectionClosed += Show;
		}

		private void OnDestroy()
		{
			collectionsButton.onClick.RemoveListener(OnCollectionsButtonClicked);
			collectionView.OnCollectionShown -= Hide;
			collectionView.OnCollectionClosed -= Show;
		}

		private void OnCollectionsButtonClicked()
		{
			_soundService.PlayUiClickSound();
			collectionView.Show();
		}

		private void Show()
		{
			gameObject.SetActive(true);
			transform.DOKill();
			
			transform
				.DOScale(Vector3.one, animationDuration);
		}

		private void Hide()
		{
			transform.DOKill();
			
			transform
				.DOScale(Vector3.zero, animationDuration)
				.OnComplete(() => gameObject.SetActive(false));
		}
	}
}