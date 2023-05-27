using Secs;
using UnityEngine;
using Zenject;

namespace Ingame
{
	[RequireComponent(typeof(Collider))]
	public sealed class CraftingSurfaceBaker : MonoBehaviour
	{
		[Inject]
		private void Construct(EcsWorldsProvider ecsWorldsProvider)
		{
			var world = ecsWorldsProvider.GameplayWorld;
			var entity = world.NewEntity();

			world.GetPool<CraftingSurfaceTag>().AddComponent(entity);
			world.GetPool<TransformMdl>().AddComponent(entity).transform = transform;
			
			this.LinkEcsEntity(world, entity);
			
			Destroy(this);
		}
	}
}