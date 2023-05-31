using Secs;
using UnityEngine;

namespace Ingame.Tasks
{
    public sealed class AllTasksMdlBaker : EcsMonoBaker
    {
        [SerializeField] private TasksConfig tasksConfig;

        protected override void Bake(EcsWorld world, int entityId)
        {
            world.GetPool<AllTasksMdl>().AddComponent(entityId).tasksConfig = tasksConfig;
        }
    }
}