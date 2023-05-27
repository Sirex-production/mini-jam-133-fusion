using NaughtyAttributes;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Shop
{
    public sealed class LockedCardBaker : MonoBehaviour
    {
        [Required, SerializeField] private Rigidbody attachedRigidbody;

        [SerializeField] private float costToUnlockTheCard;
        
        [Inject]
        private void Construct(EcsWorldsProvider worldsProvider)
        {
            var world = worldsProvider.GameplayWorld;
            int entity = worldsProvider.GameplayWorld.NewEntity();
			
            world.GetPool<IsCardTag>().AddComponent(entity);
            world.GetPool<TransformMdl>().AddComponent(entity) = new TransformMdl
            {
                transform = transform,
                initialLocalPos = transform.localPosition,
                initialLocalRot = transform.localRotation
            };
            world.GetPool<RigidbodyMdl>().AddComponent(entity).rigidbody = attachedRigidbody;
            world.GetPool<LockedCardCmp>().AddComponent(entity).moneyToUnlock = costToUnlockTheCard;
            world.UpdateFilters();
			
            this.LinkEcsEntity(world, entity);
			
            Destroy(this);
        }

    }
}