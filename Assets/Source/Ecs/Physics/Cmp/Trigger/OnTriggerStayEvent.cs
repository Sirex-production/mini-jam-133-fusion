using Secs;
using UnityEngine;

namespace Ingame
{
    public struct OnTriggerStayEvent : IEcsComponent
    {
        public Transform senderObject;
        public Collider collider;
    }
}