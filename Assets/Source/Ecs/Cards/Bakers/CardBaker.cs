using Ingame.Recipe;
using NaughtyAttributes;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame
{
	public sealed class CardBaker : MonoBehaviour
	{
		[Required, SerializeField] private ItemConfig itemConfig;
		[Required, SerializeField] private Rigidbody attachedRigidbody;
		
		[Inject]
		private void Construct(EcsWorldsProvider worldsProvider)
		{
			var world = worldsProvider.GameplayWorld;
			int entity = worldsProvider.GameplayWorld.NewEntity();
			
			world.GetPool<CardCmp>().AddComponent(entity) = new CardCmp
			{
				itemConfig = itemConfig
			};
			world.GetPool<TransformMdl>().AddComponent(entity) = new TransformMdl
			{
				transform = transform,
				initialLocalPos = transform.localPosition,
				initialLocalRot = transform.localRotation
			};
			world.GetPool<RigidbodyMdl>().AddComponent(entity).rigidbody = attachedRigidbody;
			world.UpdateFilters();
			
			this.LinkEcsEntity(world, entity);
			
			Destroy(this);
		}
		
	}
}