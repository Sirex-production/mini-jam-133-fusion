using System;
using System.Collections.Generic;
using Secs;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Ingame.Receipt
{
    public sealed class ReceiptBaker : MonoBehaviour
    {
        [SerializeField] private List<ItemConfig> startingItems;
        [SerializeField] private AllReceiptsContainerConfig allReceiptsContainerConfig;
        private EcsWorld _world;

        [Inject]
        private void Construct(EcsWorldsProvider ecsWorldsProvider)
        {
            _world = ecsWorldsProvider.GameplayWorld;
        }
        
        private void Awake()
        {
            var entity = _world.NewEntity();
            _world.GetPool<AllReceiptsMdl>().AddComponent(entity).allReceiptsContainerConfig = allReceiptsContainerConfig;
            _world.GetPool<StartingItemsMdl>().AddComponent(entity).startingItems = startingItems;
            _world.GetPool<ReceiptStatusMdl>().AddComponent(entity);
            
            gameObject.LinkEcsEntity(_world,entity);
        }
    }
}