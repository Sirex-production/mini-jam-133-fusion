using UnityEngine;

namespace Ingame
{
	public sealed class GeneralCardsConfig : MonoBehaviour
	{
		[SerializeField] [Min(0f)] private float distanceFromCraftingSurfaceWhenDragging = 5f;

		public float DistanceFromCraftingSurfaceWhenDragging => distanceFromCraftingSurfaceWhenDragging;
	}
}