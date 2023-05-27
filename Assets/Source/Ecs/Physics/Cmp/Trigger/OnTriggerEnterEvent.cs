using Secs;
using UnityEngine;

namespace Ingame
{
    public struct OnTriggerEnterEvent : IEcsComponent
    {
        public Transform senderObject;
        public Collider collider;
    }
}