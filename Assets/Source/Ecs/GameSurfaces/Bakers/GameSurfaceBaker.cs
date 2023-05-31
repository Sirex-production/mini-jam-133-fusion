using Secs;

namespace Ingame
{
	public sealed class GameSurfaceBaker : EcsMonoBaker
	{
		protected override void Bake(EcsWorld world, int entityId)
		{
			world.GetPool<GameSurfaceTag>().AddComponent(entityId);
			world.GetPool<TransformMdl>().AddComponent(entityId) = new TransformMdl
			{
				transform = transform,
				initialLocalPos = transform.localPosition,
				initialLocalRot = transform.localRotation
			};
		}
	}
}