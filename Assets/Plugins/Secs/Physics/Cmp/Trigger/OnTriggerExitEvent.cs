using UnityEngine;

namespace Secs.Physics
{
    public struct OnTriggerExitEvent : IEcsComponent
    {
        public Transform senderObject;
        public Collider collider;
    }
}