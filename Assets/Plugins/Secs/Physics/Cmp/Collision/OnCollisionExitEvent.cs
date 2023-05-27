using UnityEngine;

namespace Secs.Physics
{
    public struct OnCollisionExitEvent : IEcsComponent
    {
        public Transform senderObject;
        public Collider collider;
    }
}