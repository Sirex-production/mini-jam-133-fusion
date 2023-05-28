using Secs;
using UnityEngine;

namespace Ingame.Tasks
{
    public struct FullDestroyObjectRequest : IEcsComponent
    {
        public int entityId;
        public GameObject gameObject;
    }
}