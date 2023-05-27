using Secs;
using UnityEngine;

namespace Ingame 
{
    public static class EcsPhysics  
    {
        private static EcsWorld ecsWorld;
        private static EcsSystems ecsSystems;

        private static EcsPool<OnTriggerEnterEvent> _onTriggerEnterEvent;
        private static EcsPool<OnTriggerStayEvent> _onTriggerStayEvent;
        private static EcsPool<OnTriggerExitEvent> _onTriggerExitEvent;
        
        private static EcsPool<OnCollisionEnterEvent> _onCollisionEnterEvent;
        private static EcsPool<OnCollisionStayEvent> _onCollisionStayEvent;
        private static EcsPool<OnCollisionExitEvent> _onCollisionExitEvent;
        
        internal static void BindToEcsWorld(EcsWorld world, EcsSystems systems)
        {
            _onTriggerEnterEvent = world.GetPool<OnTriggerEnterEvent>();
            _onTriggerStayEvent = world.GetPool<OnTriggerStayEvent>();
            _onTriggerExitEvent = world.GetPool<OnTriggerExitEvent>();
            
            _onCollisionEnterEvent = world.GetPool<OnCollisionEnterEvent>();
            _onCollisionStayEvent = world.GetPool<OnCollisionStayEvent>();
            _onCollisionExitEvent = world.GetPool<OnCollisionExitEvent>();
            
            ecsWorld = world;
            ecsSystems = systems;
        }
        
        internal static void UnbindToEcsWorld()
        {
            _onTriggerEnterEvent = null;
            _onTriggerStayEvent = null;
            _onTriggerExitEvent = null;
            
            _onCollisionEnterEvent = null;
            _onCollisionStayEvent = null;
            _onCollisionExitEvent = null;
            
            ecsWorld = null;
            ecsSystems = null;
        }
        
        internal static void RegisterTriggerEnterEvent(Transform senderGameObject, Collider collider)
        {
            var eventEntity = ecsWorld.NewEntity();
            ref var eventComponent = ref _onTriggerEnterEvent.AddComponent(eventEntity);
            eventComponent.collider = collider;
            eventComponent.senderObject = senderGameObject;
        }    
        
        internal static void RegisterTriggerStayEvent(Transform senderGameObject, Collider collider)
        {
            var eventEntity = ecsWorld.NewEntity();
            ref var eventComponent = ref _onTriggerStayEvent.AddComponent(eventEntity);
            eventComponent.collider = collider;
            eventComponent.senderObject = senderGameObject;
        }    
        
        internal static void RegisterTriggerExitEvent(Transform senderGameObject, Collider collider)
        {
            var eventEntity = ecsWorld.NewEntity();
            ref var eventComponent = ref _onTriggerExitEvent.AddComponent(eventEntity);
            eventComponent.collider = collider;
            eventComponent.senderObject = senderGameObject;
        }    
        
        internal static void RegisterCollisionEnterEvent(Transform senderGameObject, Collider collider)
        {
            var eventEntity = ecsWorld.NewEntity();
            ref var eventComponent = ref _onCollisionEnterEvent.AddComponent(eventEntity);
            eventComponent.collider = collider;
            eventComponent.senderObject = senderGameObject;
        }    
        
        internal static void RegisterCollisionStayEvent(Transform senderGameObject, Collider collider)
        {
            var eventEntity = ecsWorld.NewEntity();
            ref var eventComponent = ref _onCollisionStayEvent.AddComponent(eventEntity);
            eventComponent.collider = collider;
            eventComponent.senderObject = senderGameObject;
        }    
        
        internal static void RegisterCollisionExitEvent(Transform senderGameObject, Collider collider)
        {
            var eventEntity = ecsWorld.NewEntity();
            ref var eventComponent = ref _onCollisionExitEvent.AddComponent(eventEntity);
            eventComponent.collider = collider;
            eventComponent.senderObject = senderGameObject;
        }    
    }
}