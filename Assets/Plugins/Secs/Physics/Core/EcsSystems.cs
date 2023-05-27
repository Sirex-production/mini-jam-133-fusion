using Secs.Physics;

namespace Secs
{
    public sealed partial class EcsSystems
    {
        public void AttachPhysics()
        {
            EcsPhysics.BindToEcsWorld(_world,this);
        }

        public void ReleasePhysics()
        {
            EcsPhysics.UnbindToEcsWorld();
        }
    }
}