using Secs;

namespace Ingame
{
    public sealed class DisposeOnTickPhysicsSys : IEcsRunSystem
    {
        [EcsInject] private EcsWorld _ecsWorld;
        
        [EcsInject(typeof(OnTriggerEnterEvent))]
        private EcsFilter _onTriggerEnterFilter;
        [EcsInject(typeof(OnTriggerStayEvent))]
        private EcsFilter _onTriggerStayEventFilter;
        [EcsInject(typeof(OnTriggerExitEvent))]
        private EcsFilter _onTriggerExitEventFilter;

        [EcsInject(typeof(OnCollisionEnterEvent))]
        private EcsFilter _onCollisionEnterEventFilter;
        [EcsInject(typeof(OnCollisionStayEvent))]
        private EcsFilter _onCollisionStayEventFilter;
        [EcsInject(typeof(OnCollisionExitEvent))]
        private EcsFilter _onCollisionExitEventFilter;
        
        public void OnRun()
        {
            foreach (var entity in _onTriggerEnterFilter)
                _ecsWorld.DelEntity(entity);
            
            foreach (var entity in _onTriggerStayEventFilter)
                _ecsWorld.DelEntity(entity);
            
            foreach (var entity in _onTriggerExitEventFilter)
                _ecsWorld.DelEntity(entity);

            foreach (var entity in _onCollisionEnterEventFilter)
                _ecsWorld.DelEntity(entity);
            
            foreach (var entity in _onCollisionStayEventFilter)
                _ecsWorld.DelEntity(entity);
            
            foreach (var entity in _onCollisionExitEventFilter)
                _ecsWorld.DelEntity(entity);
        }
    }
}