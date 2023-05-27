using UnityEngine;

namespace Secs.Physics
{
    public struct OnTriggerStayEvent : IEcsComponent
    {
        public Transform senderObject;
        public Collider collider;
    }
}