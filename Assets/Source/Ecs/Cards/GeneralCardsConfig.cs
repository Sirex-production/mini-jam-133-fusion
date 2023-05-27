using UnityEngine;

namespace Ingame
{
	[CreateAssetMenu(menuName = "Ingame/General Cards Config")]
	public sealed class GeneralCardsConfig : ScriptableObject
	{
		[SerializeField] [Min(0f)] private float offsetFromCraftingSurfaceWhenDragging = 5f;
		[SerializeField] [Range(0, 1)] private float cardsFollowCursorDumping = .01f;
		[SerializeField] [Range(0, 1)] private float cardsRotationDumping = .01f;

		public float OffsetFromCraftingSurfaceWhenDragging => offsetFromCraftingSurfaceWhenDragging;
		public float CardsFollowCursorDumping => cardsFollowCursorDumping;
		public float CardsRotationDumping => cardsRotationDumping;
	}
}