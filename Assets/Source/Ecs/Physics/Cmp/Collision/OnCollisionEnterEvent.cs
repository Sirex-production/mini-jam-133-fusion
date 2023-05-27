using Secs;
using UnityEngine;

namespace Ingame 
{
    public struct OnCollisionEnterEvent : IEcsComponent
    {
        public Transform senderObject;
        public Collider collider;
    }
}