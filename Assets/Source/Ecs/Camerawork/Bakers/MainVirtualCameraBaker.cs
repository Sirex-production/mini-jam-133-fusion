using Cinemachine;
using NaughtyAttributes;
using Secs;
using UnityEngine;

namespace Ingame
{
	public sealed class MainVirtualCameraBaker : EcsMonoBaker
	{
		[Required, SerializeField] private CinemachineVirtualCamera virtualCamera;
		[SerializeField] private Vector3 cameraMovementBounds;

		protected override void Bake(EcsWorld world, int entityId)
		{
			world.GetPool<VirtualCameraMdl>().AddComponent(entityId).virtualCamera = virtualCamera;
			world.GetPool<MainVirtualCameraTag>().AddComponent(entityId);
			world.GetPool<TransformMdl>().AddComponent(entityId) = new TransformMdl
			{
				transform = transform,
				initialLocalPos = transform.localPosition,
				initialLocalRot = transform.localRotation
			};
			world.GetPool<CameraMovementBoundsCmp>().AddComponent(entityId).movementBounds = cameraMovementBounds;
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(transform.position, cameraMovementBounds);
		}
	}
}