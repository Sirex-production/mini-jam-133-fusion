using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ingame
{
	public sealed class UiTutorialView : MonoBehaviour
	{
		[BoxGroup("Scene loading")]
		[SerializeField] [Scene] private int sceneToLoad;
		[BoxGroup("References")]
		[Required, SerializeField] private Button playButton;

		[BoxGroup("References (Animation)")]
		[Required, SerializeField] private Transform frameTransform;
		[BoxGroup("References (Animation)")]
		[Required, SerializeField] private CanvasGroup backgroundCanvasGroup;
		
		[BoxGroup("Animation properties")]
		[SerializeField] [Min(0f)] private float animationDuration = .3f;
		
		private SceneService _sceneService;
		
		[Inject]
		private void Construct(SceneService sceneService)
		{
			_sceneService = sceneService;
		}
		
		private void Awake()
		{
			playButton.onClick.AddListener(OnPlayButtonClicked);
			
			Hide();
		}

		private void OnDestroy()
		{
			playButton.onClick.RemoveListener(OnPlayButtonClicked);
		}

		private void OnPlayButtonClicked()
		{
			Hide();
			_sceneService.LoadLevel(sceneToLoad);
		}
		
		private void Hide()
		{
			backgroundCanvasGroup.DOFade(0, animationDuration / 2f);
			
			frameTransform
				.DOScale(0f, animationDuration / 2f)
				.OnComplete(() => gameObject.SetActive(false));
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