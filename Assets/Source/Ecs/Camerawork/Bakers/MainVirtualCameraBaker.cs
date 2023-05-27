using Cinemachine;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Ingame
{
	public sealed class MainVirtualCameraBaker : MonoBehaviour
	{
		[Required, SerializeField] private CinemachineVirtualCamera virtualCamera;
		[SerializeField] private Vector3 cameraMovementBounds;
		
		[Inject]
		private void Construct(EcsWorldsProvider worldsProvider)
		{
			var world = worldsProvider.GameplayWorld;
			int entity = world.NewEntity();

			world.GetPool<VirtualCameraMdl>().AddComponent(entity).virtualCamera = virtualCamera;
			world.GetPool<MainVirtualCameraTag>().AddComponent(entity);
			world.GetPool<TransformMdl>().AddComponent(entity) = new TransformMdl
			{
				transform = transform,
				initialLocalPos = transform.localPosition,
				initialLocalRot = transform.localRotation
			};
			world.GetPool<CameraMovementBoundsCmp>().AddComponent(entity).movementBounds = cameraMovementBounds;
			
			Destroy(this);
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(transform.position, cameraMovementBounds);
		}
	}
}