using Secs;
using UnityEngine;

namespace Ingame
{
    public struct OnTriggerExitEvent : IEcsComponent
    {
        public Transform senderObject;
        public Collider collider;
    }
}