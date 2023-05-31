using Secs;

namespace Ingame.Tasks
{
    public sealed class TaskTableBaker : EcsMonoBaker
    {
        protected override void Bake(EcsWorld world, int entityId)
        {
            world.GetPool<TaskTableTag>().AddComponent(entityId);
            world.GetPool<TransformMdl>().AddComponent(entityId).transform = transform;
            world.GetPool<OfferedTaskItemsCmp>().AddComponent(entityId).offeredItems = new();
        }
    }
}