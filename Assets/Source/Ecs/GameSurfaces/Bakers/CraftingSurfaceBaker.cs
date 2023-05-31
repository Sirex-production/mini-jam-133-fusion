using System;
using Secs;
using UnityEngine;

namespace Ingame
{
	[RequireComponent(typeof(Collider))]
	public sealed class CraftingSurfaceBaker : EcsMonoBaker
	{
		protected override void Bake(EcsWorld world, int entityId)
		{
			world.GetPool<CraftingSurfaceTag>().AddComponent(entityId);
			world.GetPool<TransformMdl>().AddComponent(entityId).transform = transform;
		}
	}
}