using Ingame.Recipe;
using Ingame.VfX;
using NaughtyAttributes;
using Secs;
using UnityEngine;

namespace Ingame
{
	[RequireComponent(typeof(EcsPhysicsCollisionProvider))]
	public sealed class CardBaker : EcsMonoBaker
	{
		[Required, SerializeField] private ItemConfig itemConfig;
		[Required, SerializeField] private Rigidbody attachedRigidbody;
		[Required, SerializeField] private CardView cardView;
		[Required, SerializeField] private new ParticleSystem particleSystem;

		protected override void Bake(EcsWorld world, int entityId)
		{
			world.GetPool<CardCmp>().AddComponent(entityId) = new CardCmp
			{
				itemConfig = itemConfig
			};
			world.GetPool<TransformMdl>().AddComponent(entityId) = new TransformMdl
			{
				transform = transform,
				initialLocalPos = transform.localPosition,
				initialLocalRot = transform.localRotation
			};
			world.GetPool<RigidbodyMdl>().AddComponent(entityId).rigidbody = attachedRigidbody;
			world.GetPool<CardViewMdl>().AddComponent(entityId).cardView = cardView;
			world.GetPool<ParticleSystemMdl>().AddComponent(entityId).particleSystem = particleSystem;
		}
	}
}