using UnityEngine;

namespace  Secs.Physics 
{
    public struct OnCollisionEnterEvent : IEcsComponent
    {
        public Transform senderObject;
        public Collider collider;
    }
}