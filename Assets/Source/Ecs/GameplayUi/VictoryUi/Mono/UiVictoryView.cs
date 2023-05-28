using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ingame
{
	public sealed class UiVictoryView : MonoBehaviour
	{
		[BoxGroup("Scene loading")]
		[SerializeField] [Scene] private int sceneToLoad;
		
		[BoxGroup("References")]
		[Required, SerializeField] private CanvasGroup parentCanvasGroup;
		[BoxGroup("References")]
		[Required, SerializeField] private Button mainMenuButton;
		
		[BoxGroup("Animation")]
		[SerializeField] [Min(0f)] private float animationDuration = 1f;
		
		private SceneService _sceneService;
		private SoundService _soundService;
		
		[Inject]
		private void Construct(SceneService sceneService, SoundService soundService)
		{
			_sceneService = sceneService;
			_soundService = soundService;
		}
		
		private void Awake()
		{
			mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
			gameObject.SetActive(false);
		}

		private void OnDestroy()
		{
			mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
		}

		private void OnMainMenuButtonClicked()
		{
			_soundService.PlayUiClickSound();
			_sceneService.LoadLevel(sceneToLoad);
		}

		public void Show()
		{
			
			gameObject.SetActive(true);
			parentCanvasGroup.DOFade(1f, animationDuration);
		}
	}
}