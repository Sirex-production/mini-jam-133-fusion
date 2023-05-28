using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Ingame
{
	public sealed class UiPauseMainPanel : MonoBehaviour
	{
		[BoxGroup("SceneLoading")]
		[SerializeField] [Scene] private int mainMenuScene;

		[BoxGroup("References")]
		[Required, SerializeField] private Button pauseButton;
		[BoxGroup("References")]
		[Required, SerializeField] private Button continueButton;
		[BoxGroup("References")]
		[Required, SerializeField] private Button restartTheGameButton;
		[BoxGroup("References")]
		[Required, SerializeField] private Button settingsButton;
		[BoxGroup("References")]
		[Required, SerializeField] private Button exitButton;
		
		[BoxGroup("References other views")]
		[Required, SerializeField] private UiSettingsView settingsView;
		
		[BoxGroup("References (Animation)")]
		[Required, SerializeField] private Transform parentTransform;
		[BoxGroup("References (Animation)")]
		[Required, SerializeField] private Transform contentParent;
		[BoxGroup("References (Animation)")]
		[Required, SerializeField] private CanvasGroup backgroundCanvasGroup;
		
		[BoxGroup("Animation properties")]
		[SerializeField] Vector3 showOffsetAnimation;
		[BoxGroup("Animation properties")]
		[SerializeField] [Min(0f)] float animationDuration = .3f;

		private Vector3 _initialContentParentPos;
		private SceneService _sceneService;
		private InputService _inputService;
		private SoundService _soundService;

		private bool _isOpened = true;
		
		[Inject]
		private void Construct(SceneService sceneService, InputService inputService, SoundService soundService)
		{
			_sceneService = sceneService;
			_inputService = inputService;
			_soundService = soundService;
			
			Hide();
		}

		private void Awake()
		{
			_initialContentParentPos = contentParent.position;

			pauseButton.onClick.AddListener(OnPauseButtonClicked);
			continueButton.onClick.AddListener(OnContinueButtonClicked);
			restartTheGameButton.onClick.AddListener(OnRestartButtonClicked);
			settingsButton.onClick.AddListener(OnSettingsButtonClicked);
			exitButton.onClick.AddListener(OnExitButtonClicked);
		}

		private void OnDestroy()
		{
			pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
			continueButton.onClick.RemoveListener(OnContinueButtonClicked);
			restartTheGameButton.onClick.RemoveListener(OnRestartButtonClicked);
			settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
			exitButton.onClick.RemoveListener(OnExitButtonClicked);
		}

		private void OnPauseButtonClicked()
		{
			_soundService.PlayUiClickSound();
			
			if(_isOpened) 
				Hide();
			else
				Show();
		}

		private void OnContinueButtonClicked()
		{
			_soundService.PlayUiClickSound();
			Hide();
		}
		
		private void OnRestartButtonClicked()
		{
			_soundService.PlayUiClickSound();
			_sceneService.LoadLevel(SceneManager.GetActiveScene().buildIndex);
			Hide();
		}
		
		private void OnSettingsButtonClicked()
		{
			_soundService.PlayUiClickSound();
			settingsView.Show();
		}
		
		private void OnExitButtonClicked()
		{
			_soundService.PlayUiClickSound();
			_sceneService.LoadLevel(mainMenuScene);
		}

		private void Hide()
		{
			if(!_isOpened)
				return;
			
			_isOpened = false;
			_inputService.MovementEnabled = true;

			pauseButton.gameObject.SetActive(true);
			
			backgroundCanvasGroup.DOFade(0, animationDuration);

			contentParent
				.DOMove(_initialContentParentPos + showOffsetAnimation, animationDuration)
				.OnComplete(() => parentTransform.gameObject.SetActive(false));
		}

		private void Show()
		{
			if(_isOpened)
				return;
			
			_isOpened = true;
			_inputService.MovementEnabled = false;
			
			pauseButton.gameObject.SetActive(false);
			parentTransform.gameObject.SetActive(true);
			contentParent.position = _initialContentParentPos + showOffsetAnimation;
			
			backgroundCanvasGroup.DOFade(1, animationDuration);
			
			contentParent
				.DOMove(_initialContentParentPos, animationDuration)
				.SetEase(Ease.OutBack);
		}
	}
}