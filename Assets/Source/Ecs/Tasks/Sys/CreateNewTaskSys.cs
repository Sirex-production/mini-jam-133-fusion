using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Tasks
{
    public sealed class CreateNewTaskSys : IEcsRunSystem
    {
        [EcsInject] private EcsWorld _ecsWorld;
        
        [EcsInject(typeof(AskNewTaskEvent))] 
        private readonly EcsFilter _askNewTaskFilter;
        
        [EcsInject(typeof(AllTasksMdl))] 
        private readonly EcsFilter _allTasksFilter;

        [EcsInject]
        private readonly EcsPool<AllTasksMdl> _allTasksPool;
        
        [EcsInject]
        private readonly EcsPool<TaskHolderMdl> _taskHolderPool;
        
        [EcsInject(typeof(TaskHolderMdl))] 
        private readonly EcsFilter _Filter;

        public void OnRun()
        {
            foreach (var askNewTaskEntity in _askNewTaskFilter)
            {
                Debug.LogWarning(askNewTaskEntity);
                var newEntity = _ecsWorld.NewEntity();
                ref var taskHolderMdl = ref _taskHolderPool.AddComponent(newEntity);
                
                foreach (var allTaskEntity in _allTasksFilter)
                {
                    ref var allTasksMdl = ref _allTasksPool.GetComponent(allTaskEntity);
                    var allTasks = allTasksMdl.tasksConfig.Tasks;
                    
                    int randomIndex = Random.Range(0, allTasks.Count);
                    
                    taskHolderMdl.currentTask = allTasks[randomIndex];
                }
                
                _ecsWorld.DelEntity(askNewTaskEntity);
            }

            foreach (var f in _Filter)
            {
            
            }
        }
    }
}