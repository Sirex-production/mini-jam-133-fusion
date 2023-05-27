using NaughtyAttributes;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame
{
	public sealed class CardBaker : MonoBehaviour
	{
		[Required, SerializeField] private Rigidbody attachedRigidbody;
		
		[Inject]
		private void Construct(EcsWorldsProvider worldsProvider)
		{
			var world = worldsProvider.GameplayWorld;
			int entity = worldsProvider.GameplayWorld.NewEntity();
			
			world.GetPool<IsCardTag>().AddComponent(entity);
			world.GetPool<TransformMdl>().AddComponent(entity).transform = transform;
			world.GetPool<RigidbodyMdl>().AddComponent(entity).rigidbody = attachedRigidbody;

			world.UpdateFilters();
			
			this.LinkEcsEntity(world, entity);
			
			Destroy(this);
		}
	}
}