using DG.Tweening;
using UnityEngine;

namespace Ingame
{
	public sealed class UiButtonScaler : MonoBehaviour
	{
		[SerializeField] private Vector3 scaleWhenPointerEnter;
		[SerializeField] [Min(0f)] private float animationDuration = .3f;

		private Vector3 _initialLocalScale;
		
		private void Awake()
		{
			_initialLocalScale = transform.localScale;
		}

		public void OnPointerEnter()
		{
			transform.DOKill();
			transform
				.DOScale(scaleWhenPointerEnter, animationDuration)
				.SetLink(gameObject);
		}
		
		public void OnPointerExit()
		{
			transform.DOKill();
			transform
				.DOScale(_initialLocalScale, animationDuration)
				.SetLink(gameObject);
		}
	}
}