﻿using System;
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
		
		[BoxGroup("Animation properties")]
		[SerializeField] [Min(0f)] private float animationDuration = .3f;
		
		private SoundService _soundService;
		
		public event Action OnDevelopersClosed;

		[Inject]
		private void Construct(SoundService soundService)
		{
			_soundService = soundService;
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
			_soundService.PlayUiClickSound();
			Hide();
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