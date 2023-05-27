using System.Collections.Generic;
using Secs;
using UnityEngine;

namespace Ingame.Npc
{
    public struct WaypointsCmp : IEcsComponent
    {
        public List<Transform> transforms;
        public int index;

        public Transform Next()
        {
            index++;
            index %= transforms.Count;
            return transforms[index];
        }
        
    }
}