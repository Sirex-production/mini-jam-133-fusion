using Secs;

namespace Ingame.Tasks
{
    public struct AllTasksMdl : IEcsComponent
    {
        public TasksConfig tasksConfig;
        private int _index;
        public Task GetNewTask()
        {
            var task = tasksConfig.Tasks[_index++];
            _index %= tasksConfig.Tasks.Count;
            return task;
        }
    }
}