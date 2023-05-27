using Secs;
using UnityEngine;

namespace Ingame
{
    public struct OnCollisionStayEvent : IEcsComponent
    {
        public Transform senderObject;
        public Collider collider;
    }
}