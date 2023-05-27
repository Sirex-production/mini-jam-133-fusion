using Secs;
using UnityEngine;

namespace Ingame
{
	public struct ShopCmp : IEcsComponent
	{
		public CardBaker cardPrefab;
		public Transform[] slotsPositions;
	}
}