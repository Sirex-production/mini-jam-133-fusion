using DG.Tweening;
using Secs;

namespace Ingame.Npc
{
    public sealed class MoveTaskNpcSys : IEcsRunSystem
    {
        [EcsInject] private EcsWorld _ecsWorld;
        
        [EcsInject(typeof(WaypointsCmp),typeof(TaskNpcTag),typeof(TransformMdl))] 
        private EcsFilter _npcFilter;
        
        [EcsInject(typeof(MoveBackNpcEvent))] 
        private EcsFilter _backFilter;
        
        [EcsInject(typeof(ForwardNpcEvent))]
        private EcsFilter _forwardFilter;

        [EcsInject]
        private EcsPool<WaypointsCmp> _waypointPool;
        
        [EcsInject]
        private EcsPool<TransformMdl> _transformMdlPool;
        
        [EcsInject]
        private EcsPool<IsUnderDOTweenAnimationTag> _isUnderDOTweenAnimationTagPool;
        
        public void OnRun()
        {
            foreach (var backEntity in _backFilter)
            {
                PerformNpcAnimations();
                _ecsWorld.DelEntity(backEntity);
            }
            
            foreach (var forwardEntity in _forwardFilter)
            {
                PerformNpcAnimations();
                _ecsWorld.DelEntity(forwardEntity);
            }
        }
        
        private void PerformNpcAnimations()
        {
            if(_npcFilter.IsEmpty)
                return;
            
            var npcEntity = _npcFilter.GetFirstEntity();
            
            if(_isUnderDOTweenAnimationTagPool.HasComponent(npcEntity))
                return;
            
            ref var waypoint = ref _waypointPool.GetComponent(npcEntity);
            ref var transformMdl = ref _transformMdlPool.GetComponent(npcEntity);
            
            _isUnderDOTweenAnimationTagPool.AddComponent(npcEntity);
            
            transformMdl.transform.DOJump(waypoint.Next().position, 2, 7, 3)	
                .SetEase(Ease.Linear)
                .SetLink(transformMdl.transform.gameObject)
                .OnComplete(() => _isUnderDOTweenAnimationTagPool.DelComponent(npcEntity));
        }
    }
}