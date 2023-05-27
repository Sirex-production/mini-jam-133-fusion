using System.Collections.Generic;
using Secs;

namespace Ingame.Tasks
{
    public struct UnlockedTasksMdl : IEcsComponent
    {
        public List<Task> tasks;
    }
}