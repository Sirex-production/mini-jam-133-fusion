using System.Linq;
using Ingame.Recipe;
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
        private readonly EcsFilter _taskHolderFilter;
        
        [EcsInject(typeof(RecipeStatusMdl))] 
        private readonly EcsFilter _recipeStatusMdlFilter;
        

        [EcsInject]
        private readonly EcsPool<AllTasksMdl> _allTasksPool;
        
        [EcsInject]
        private readonly EcsPool<TaskHolderMdl> _taskHolderPool;
        
        [EcsInject]
        private readonly EcsPool<AskNewTaskEvent> _askNewTaskPool;
        
        [EcsInject]
        private readonly EcsPool<RecipeStatusMdl> _recipeStatusMdlPool;
     
        
        public void OnRun()
        {
            foreach (var askNewTaskEntity in _askNewTaskFilter)
            {
                if (_taskHolderFilter.IsEmpty || _allTasksFilter.IsEmpty || _recipeStatusMdlFilter.IsEmpty)
               {
                   _ecsWorld.DelEntity(askNewTaskEntity);
                   continue;
               }
                
               ref var taskHolderMdl = ref _taskHolderPool.GetComponent(_taskHolderFilter.GetFirstEntity());
               ref var allTasksMdl = ref _allTasksPool.GetComponent(_allTasksFilter.GetFirstEntity());
               ref var recipeStatusMdl = ref _recipeStatusMdlPool.GetComponent(_recipeStatusMdlFilter.GetFirstEntity());

               var possibleItem = recipeStatusMdl.unlockedRecipe.Select(e => e.CreatedItem).ToList();
               
               var allTasks = allTasksMdl.tasksConfig.Tasks;

               var possibleTasks = allTasks.Where(e => e.QuestItems.All(questItem => possibleItem.Contains(questItem))).ToList();
               
               int randomIndex = Random.Range(0, possibleTasks.Count);
               taskHolderMdl.currentTask = possibleTasks[randomIndex];

               _ecsWorld.DelEntity(askNewTaskEntity);
            }
        }
    }
}