using System;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Shop
{
    public sealed class Basket : MonoBehaviour
    {
        private EcsPool<TryUnlockCardReq> _pool;
        private void OnCollisionEnter(Collision collision)
        {
            if(!collision.collider.TryGetComponent<EcsEntityReference>(out var ecsEntityReference))
                return;
            
            _pool ??= ecsEntityReference.World.GetPool<TryUnlockCardReq>();

            if (!_pool.HasComponent(ecsEntityReference.EntityId))
                _pool.AddComponent(ecsEntityReference.EntityId);
            
        }
    }
}