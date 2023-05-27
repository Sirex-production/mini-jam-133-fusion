using Secs;
using UnityEngine;

namespace Ingame
{
    public struct OnCollisionExitEvent : IEcsComponent
    {
        public Transform senderObject;
        public Collider collider;
    }
}