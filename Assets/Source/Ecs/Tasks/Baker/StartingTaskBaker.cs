using Ingame.Recipe;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Tasks
{
    public sealed class StartingTaskBaker : MonoBehaviour
    {
        private EcsWorld _ecsWorld;
        
        [Inject]
        private void Construct(EcsWorldsProvider ecsWorldsProvider)
        {
            _ecsWorld = ecsWorldsProvider.GameplayWorld;
        }
        
        private void Awake()
        {
            var entity = _ecsWorld.NewEntity();
            _ecsWorld.GetPool<AskNewTaskEvent>().AddComponent(entity);
            
            _ecsWorld.UpdateFilters();
        }
    }
}