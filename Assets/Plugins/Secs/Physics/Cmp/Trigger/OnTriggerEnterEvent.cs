using UnityEngine;

namespace Secs.Physics
{
    public struct OnTriggerEnterEvent : IEcsComponent
    {
        public Transform senderObject;
        public Collider collider;
    }
}