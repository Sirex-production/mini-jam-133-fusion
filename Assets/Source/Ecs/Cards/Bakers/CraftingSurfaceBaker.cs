using UnityEngine;
using Zenject;

namespace Ingame
{
	public sealed class CraftingSurfaceBaker : MonoBehaviour
	{
		[Inject]
		private void Construct(EcsWorldsProvider ecsWorldsProvider)
		{
			var world = ecsWorldsProvider.GameplayWorld;
			
			int entity = world.NewEntity();
			
			world.GetPool<CraftingSurfaceTag>().AddComponent(entity);
			world.GetPool<TransformMdl>().AddComponent(entity) = new TransformMdl
			{
				transform = transform,
				initialLocalPos = transform.localPosition,
				initialLocalRot = transform.localRotation
			};
		}
	}
}