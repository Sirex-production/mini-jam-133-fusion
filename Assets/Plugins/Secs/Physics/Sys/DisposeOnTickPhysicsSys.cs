namespace Secs.Physics
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
        
        [EcsInject]
        private EcsPool<OnTriggerEnterEvent> _onTriggerEnterEvent;
        [EcsInject]
        private EcsPool<OnTriggerStayEvent> _onTriggerStayEvent;
        [EcsInject]
        private EcsPool<OnTriggerExitEvent> _onTriggerExitEvent;
        
        [EcsInject]
        private EcsPool<OnCollisionEnterEvent> _onCollisionEnterEvent;
        [EcsInject]
        private EcsPool<OnCollisionStayEvent> _onCollisionStayEvent;
        [EcsInject]
        private EcsPool<OnCollisionExitEvent> _onCollisionExitEvent;
        public void OnRun()
        {
            foreach (var entity in _onTriggerEnterFilter)
                _ecsWorld.DelEntity(entity);
            
            foreach (var entity in _onTriggerStayEventFilter)
                _ecsWorld.DelEntity(entity);
            
            foreach (var entity in _onCollisionExitEventFilter)
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