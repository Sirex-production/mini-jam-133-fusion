using UnityEngine;

namespace Secs.Physics
{
    public struct OnCollisionStayEvent : IEcsComponent
    {
        public Transform senderObject;
        public Collider collider;
    }
}