using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Ingame
{
	public sealed class UiLoadingScreen : MonoBehaviour
	{
		[Required, SerializeField] private CanvasGroup canvasGroup;
		[SerializeField] [Min(0f)] private float animationDuration = .1f;

		private SceneService _sceneService;
		
		[Inject]
		private void Construct(SceneService sceneService)
		{
			_sceneService = sceneService;
		}
		
		private void Awake()
		{
			_sceneService.OnLoadingStarted += Show;
			_sceneService.OnLoadingDone += Hide;

			Hide();
		}

		private void OnDestroy()
		{
			_sceneService.OnLoadingStarted -= Show;
			_sceneService.OnLoadingDone -= Hide;
		}
		
		private void Show()
		{
			canvasGroup.alpha = 0f;
			gameObject.SetActive(true);
			canvasGroup.DOFade(1f, animationDuration);
		}
		
		private void Hide()
		{
			canvasGroup
				.DOFade(0f, animationDuration)
				.OnComplete(() => gameObject.SetActive(false));
		}
	}
}