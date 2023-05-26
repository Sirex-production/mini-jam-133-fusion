﻿using System;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Tasks
{
    public sealed class AllTasksMdlBaker : MonoBehaviour
    {
        [SerializeField] private TasksConfig tasksConfig;
            
        private EcsWorld _world;
        
        [Inject]
        private void Construct(EcsWorldsProvider ecsWorldsProvider)
        {
            _world = ecsWorldsProvider.GameplayWorld;
        }

        private void Awake()
        {
           var entity =  _world.NewEntity();
           _world.GetPool<AllTasksMdl>().AddComponent(entity).tasksConfig = tasksConfig;
           
           _world.UpdateFilters();
        }
    }
}