using UnityEngine;

namespace Ingame
{
	[CreateAssetMenu(menuName = "Ingame/General Cards Config")]
	public sealed class GeneralCardsConfig : ScriptableObject
	{
		[SerializeField] [Min(0f)] private float offsetFromCraftingSurfaceWhenDragging = 5f;
		[SerializeField] [Range(0, 1)] private float cardsFollowCursorDumping = .01f;

		public Vector3 OffsetFromCraftingSurfaceWhenDragging => offsetFromCraftingSurfaceWhenDragging * Vector3.up;
		public float CardsFollowCursorDumping => cardsFollowCursorDumping;
	}
}