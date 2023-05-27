using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Tasks
{
    public sealed class CreateNewTaskSys : IEcsRunSystem
    {
        [EcsInject] private readonly EcsWorld _ecsWorld;
        
        [EcsInject(typeof(AskNewTaskEvent))] 
        private readonly EcsFilter _askNewTaskFilter;
        
        [EcsInject(typeof(AllTasksMdl))] 
        private readonly EcsFilter _allTasksFilter;
        
        [EcsInject(typeof(TaskHolderMdl))] 
        private readonly EcsFilter _filter;

        [EcsInject]
        private readonly EcsPool<AllTasksMdl> _allTasksPool;
        
        [EcsInject]
        private readonly EcsPool<TaskHolderMdl> _taskHolderPool;
        
        [EcsInject]
        private readonly EcsPool<AskNewTaskEvent> _askNewTaskPool;
        
        public void OnRun()
        {
            foreach (var askNewTaskEntity in _askNewTaskFilter)
            {
                ref var taskHolderMdl = ref _taskHolderPool.AddComponent(askNewTaskEntity);
                
                foreach (var allTaskEntity in _allTasksFilter)
                {
                    ref var allTasksMdl = ref _allTasksPool.GetComponent(allTaskEntity);
                    var allTasks = allTasksMdl.tasksConfig.Tasks;
                    
                    int randomIndex = Random.Range(0, allTasks.Count);
                    
                    taskHolderMdl.currentTask = allTasks[randomIndex];
                }
                
                _askNewTaskPool.DelComponent(askNewTaskEntity);
            }
        }
    }
}