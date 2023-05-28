using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ingame
{
	public sealed class UiSettingsView : MonoBehaviour
	{
		[BoxGroup("References")]
		[Required, SerializeField] private Button closeButton;
		[BoxGroup("References")]
		[Required, SerializeField] private Slider soundVolumeSlider;
		[BoxGroup("References")]
		[Required, SerializeField] private Slider cameraMovementSpeedSlider;
		[BoxGroup("References")]
		[Required, SerializeField] private Slider mouseDragSensitivitySlider;
		
		[BoxGroup("References (Animation)")]
		[Required, SerializeField] private Transform frameTransform;
		[BoxGroup("References (Animation)")]
		[Required, SerializeField] private CanvasGroup backgroundCanvasGroup;
		
		[BoxGroup("Animation properties")]
		[SerializeField] [Min(0f)] private float animationDuration = .3f;

		private SettingsService _settingsService;
		
		public event Action OnSettingsClosed;

		[Inject]
		private void Construct(SettingsService settingsService)
		{
			_settingsService = settingsService;
		}
		
		private void Awake()
		{
			closeButton.onClick.AddListener(Hide);
			soundVolumeSlider.onValueChanged.AddListener(OnSoundVolumeSliderValueChanged);
			cameraMovementSpeedSlider.onValueChanged.AddListener(OnCameraMovementSpeedSliderValueChanged);
			mouseDragSensitivitySlider.onValueChanged.AddListener(OnMouseDragSensitivitySliderValueChanged);

			var currentSettingsData = _settingsService.currentSettingsData;
			
			soundVolumeSlider.value = currentSettingsData.soundVolume;
			cameraMovementSpeedSlider.value = currentSettingsData.cameraMovementSpeed;
			mouseDragSensitivitySlider.value = currentSettingsData.mapMouseDraggingSpeed;
			
			Hide();
		}

		private void OnDestroy()
		{
			closeButton.onClick.RemoveListener(Hide);
			soundVolumeSlider.onValueChanged.RemoveListener(OnSoundVolumeSliderValueChanged);
			cameraMovementSpeedSlider.onValueChanged.RemoveListener(OnCameraMovementSpeedSliderValueChanged);
			mouseDragSensitivitySlider.onValueChanged.RemoveListener(OnMouseDragSensitivitySliderValueChanged);
		}

		private void OnSoundVolumeSliderValueChanged(float value)
		{
			_settingsService.currentSettingsData.soundVolume = value;
		}

		private void OnCameraMovementSpeedSliderValueChanged(float value)
		{
			_settingsService.currentSettingsData.cameraMovementSpeed = value;
		}

		private void OnMouseDragSensitivitySliderValueChanged(float value)
		{
			_settingsService.currentSettingsData.mapMouseDraggingSpeed = value;
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
						OnSettingsClosed?.Invoke();
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