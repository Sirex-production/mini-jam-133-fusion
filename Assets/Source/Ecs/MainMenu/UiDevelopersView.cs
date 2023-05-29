using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ingame
{
	public sealed class UiDevelopersView : MonoBehaviour
	{
		[BoxGroup("References")]
		[Required, SerializeField] private Button closeButton;
		[BoxGroup("References")]
		[Required, SerializeField] private Transform frameTransform;
		[BoxGroup("References")]
		[Required, SerializeField] private CanvasGroup backgroundCanvasGroup;
		
		[BoxGroup("References (Social media)")]
		[Required, SerializeField] private Button discordButton;
		[BoxGroup("References (Social media)")]
		[Required, SerializeField] private Button twitterButton;
		
		[BoxGroup("Animation properties")]
		[SerializeField] [Min(0f)] private float animationDuration = .3f;
		
		private SoundService _soundService;
		private SocialMediaConfig _socialMediaConfig;
		
		public event Action OnDevelopersClosed;

		[Inject]
		private void Construct(SoundService soundService, SocialMediaConfig socialMediaConfig)
		{
			_soundService = soundService;
			_socialMediaConfig = socialMediaConfig;
		}
		
		private void Awake()
		{
			closeButton.onClick.AddListener(OnCloseButtonClicked);
			discordButton.onClick.AddListener(OnDiscordButtonClicked);
			twitterButton.onClick.AddListener(OnTwitterButtonClicked);
			Hide();
		}

		private void OnDestroy()
		{
			closeButton.onClick.RemoveListener(OnCloseButtonClicked);
			discordButton.onClick.RemoveListener(OnDiscordButtonClicked);
			twitterButton.onClick.RemoveListener(OnTwitterButtonClicked);
		}

		private void OnCloseButtonClicked()
		{
			_soundService.PlayUiClickSound();
			Hide();
		}
		
		private void OnDiscordButtonClicked()
		{
			_soundService.PlayUiClickSound();
			Application.OpenURL(_socialMediaConfig.DiscordLink);
		}
		
		private void OnTwitterButtonClicked()
		{
			_soundService.PlayUiClickSound();
			Application.OpenURL(_socialMediaConfig.TwitterLink);
		}
		
		private void Hide()
		{
			backgroundCanvasGroup.DOFade(0, animationDuration / 2f);
			
			frameTransform
				.DOScale(0f, animationDuration / 2f)
				.OnComplete(
					() =>
					{
						gameObject.SetActive(false);
						OnDevelopersClosed?.Invoke();
					});
		}

		public void Show()
		{
			gameObject.SetActive(true);

			backgroundCanvasGroup.DOFade(1, animationDuration);
			
			frameTransform.localScale = Vector3.zero;
			frameTransform
				.DOScale(1f, animationDuration)
				.SetEase(Ease.OutBack);
		}
	}
}