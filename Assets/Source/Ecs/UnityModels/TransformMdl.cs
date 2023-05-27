using Secs;
using UnityEngine;

namespace Ingame
{
	public struct TransformMdl : IEcsComponent
	{
		public Vector3 initialLocalPos;
		public Quaternion initialLocalRot;
		public Transform transform;
	}
}