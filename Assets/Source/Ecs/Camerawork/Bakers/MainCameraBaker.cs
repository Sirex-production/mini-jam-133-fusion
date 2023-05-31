using NaughtyAttributes;
using Secs;
using UnityEngine;

namespace Ingame
{
	public sealed class MainCameraBaker : EcsMonoBaker
	{
		[Required, SerializeField] private Camera mainCamera;
		
		protected override void Bake(EcsWorld world, int entityId)
		{
			world.GetPool<MainCameraTag>().AddComponent(entityId);
			world.GetPool<CameraMdl>().AddComponent(entityId).camera = mainCamera;
			world.GetPool<TransformMdl>().AddComponent(entityId) = new TransformMdl
			{
				transform = transform,
				initialLocalPos = transform.localPosition,
				initialLocalRot = transform.localRotation
			};
		}
	}
}