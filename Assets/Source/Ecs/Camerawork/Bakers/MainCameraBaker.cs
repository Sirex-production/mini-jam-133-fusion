using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Ingame
{
	public sealed class MainCameraBaker : MonoBehaviour
	{
		[Required, SerializeField] private Camera mainCamera;
		
		[Inject]
		public void Construct(EcsWorldsProvider ecsWorldsProvider)
		{
			var world = ecsWorldsProvider.GameplayWorld;
			
			int entity = world.NewEntity();
			
			world.GetPool<MainCameraTag>().AddComponent(entity);
			world.GetPool<CameraMdl>().AddComponent(entity).camera = mainCamera;
			world.GetPool<TransformMdl>().AddComponent(entity).transform = transform;

			Destroy(this);
		}
	}
}