using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ingame
{
	public sealed class UiMainMenuMainPanelView : MonoBehaviour
	{
		[BoxGroup("SceneLoading")]
		[SerializeField] [Scene] private int sceneToLoad;
		
		[BoxGroup("References")]
		[Required, SerializeField] private Button startGameButton;
		[BoxGroup("References")]
		[Required, SerializeField] private Button developersButton;
		[BoxGroup("References")]
		[Required, SerializeField] private Button settingsButton;
		[BoxGroup("References")]
		[Required, SerializeField] private Button exitButton;
		
		[BoxGroup("References other views")]
		[Required, SerializeField] private UiDevelopersView developersView;
		[BoxGroup("References other views")]
		[Required, SerializeField] private UiSettingsView settingsView;
		
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
		
		[Inject]
		private void Construct(SceneService sceneService)
		{
			_sceneService = sceneService;
		}

		private void Awake()
		{
			_initialContentParentPos = contentParent.position;
			
			startGameButton.onClick.AddListener(OnPlayButtonClicked);
			developersButton.onClick.AddListener(OnDevelopersButtonClicked);
			settingsButton.onClick.AddListener(OnSettingsButtonClicked);
			exitButton.onClick.AddListener(OnExitButtonClicked);
			
			developersView.OnDevelopersClosed += Show;
			settingsView.OnSettingsClosed += Show;
			
			Show();
		}

		private void OnDestroy()
		{
			startGameButton.onClick.RemoveListener(OnPlayButtonClicked);
			developersButton.onClick.RemoveListener(OnDevelopersButtonClicked);
			settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
			exitButton.onClick.RemoveListener(OnExitButtonClicked);
			
			developersView.OnDevelopersClosed -= Show;
			settingsView.OnSettingsClosed -= Show;
		}

		private void OnPlayButtonClicked()
		{
			_sceneService.LoadLevel(sceneToLoad);
		}
		
		private void OnDevelopersButtonClicked()
		{
			Hide();
			developersView.Show();
		}
		
		private void OnSettingsButtonClicked()
		{
			Hide();
			settingsView.Show();
		}
		
		private void OnExitButtonClicked()
		{
			Application.Quit();
		}

		private void Hide()
		{
			backgroundCanvasGroup.DOFade(0, animationDuration);
			
			contentParent
				.DOMove(_initialContentParentPos + showOffsetAnimation, animationDuration)
				.OnComplete(() => gameObject.SetActive(false));
		}

		private void Show()
		{
			gameObject.SetActive(true);
			contentParent.position = _initialContentParentPos + showOffsetAnimation;

			backgroundCanvasGroup.DOFade(1, animationDuration);
			
			contentParent
				.DOMove(_initialContentParentPos, animationDuration)
				.SetEase(Ease.OutBack);
		}
	}
}