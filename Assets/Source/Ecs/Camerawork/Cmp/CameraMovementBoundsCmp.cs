using Secs;
using UnityEngine;

namespace Ingame
{
	public struct CameraMovementBoundsCmp : IEcsComponent
	{
		public Vector3 movementBounds;
	}
}