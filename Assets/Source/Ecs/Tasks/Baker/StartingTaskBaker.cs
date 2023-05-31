using Secs;

namespace Ingame.Tasks
{
    public sealed class StartingTaskBaker : EcsMonoBaker
    {
        protected override void Bake(EcsWorld world, int entityId)
        {
            world.GetPool<AskNewTaskEvent>().AddComponent(entityId);
        }
    }
}