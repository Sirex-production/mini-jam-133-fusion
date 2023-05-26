using System;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Shop
{
    public sealed class WalletCmpBaker : MonoBehaviour
    {
        private EcsWorld _world;
        
        [Inject]
        private void Construct(EcsWorldsProvider ecsWorldsProvider)
        {
            _world = ecsWorldsProvider.GameplayWorld;
        }

        private void Awake()
        {
            var entity = _world.NewEntity();
            _world.GetPool<WalletCmp>().AddComponent(entity);
            
            _world.UpdateFilters();
            
        }
    }
}