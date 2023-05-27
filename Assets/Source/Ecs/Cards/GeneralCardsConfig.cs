using UnityEngine;

namespace Ingame
{
	[CreateAssetMenu(menuName = "Ingame/General Cards Config")]
	public sealed class GeneralCardsConfig : ScriptableObject
	{
		[SerializeField] [Min(0f)] private float offsetFromCraftingSurfaceWhenDragging = 5f;
		[SerializeField] [Range(0, 1)] private float cardsFollowCursorDumping = .01f;
		[SerializeField] [Range(0, 90)] private float cardsRotationAngle = 20f;
		[SerializeField] [Range(0, 100)] private float cardsRotationStrength = 20f;
		
		[SerializeField] [Min(0f)] private float timeShouldPassToBeInRestState = 5f;

		public float OffsetFromCraftingSurfaceWhenDragging => offsetFromCraftingSurfaceWhenDragging;
		public float CardsFollowCursorDumping => cardsFollowCursorDumping;
		public float CardsRotationAngle => cardsRotationAngle;
		public float CardsRotationStrength => cardsRotationStrength;

		public float TimeShouldPassToBeInRestState => timeShouldPassToBeInRestState;
	}
}