using System;
using DG.Tweening;
using Ingame.Audio;
using Ingame.Tasks;
using Secs;
using UnityEngine;

namespace Ingame.Npc
{
    public sealed class MoveTaskNpcSys : IEcsRunSystem
    {
        private SoundService _soundService;
        
        [EcsInject] private EcsWorld _ecsWorld;
        
        [EcsInject(typeof(WaypointsCmp),typeof(TaskNpcTag),typeof(TransformMdl),typeof(DialogMdl),typeof(AudioCmp))] 
        private EcsFilter _npcFilter;
        
        [EcsInject(typeof(MoveBackNpcEvent))] 
        private EcsFilter _backFilter;
        
        [EcsInject(typeof(ForwardNpcEvent))]
        private EcsFilter _forwardFilter;

        [EcsInject(typeof(TaskHolderMdl))]
        private EcsFilter _taskHolderMdlFilter;
        
        [EcsInject]
        private EcsPool<WaypointsCmp> _waypointPool;
        
        [EcsInject]
        private EcsPool<TransformMdl> _transformMdlPool;
        
        [EcsInject]
        private EcsPool<TaskHolderMdl> _taskHolderMdlPool;
        
        [EcsInject]
        private EcsPool<AudioCmp> _audioClipPool;
        
        [EcsInject]
        private EcsPool<IsUnderDOTweenAnimationTag> _isUnderDOTweenAnimationTagPool;

        [EcsInject] private EcsPool<ForwardNpcEvent> _forwardNpcEventPool;

        [EcsInject]
        private EcsPool<DialogMdl> _dialogMdlPool;
        
        
        public MoveTaskNpcSys(SoundService soundService)
        {
            _soundService = soundService;
        }

        
        public void OnRun()
        {
            foreach (var forwardEntity in _forwardFilter)
            {
                StartTask();
                _ecsWorld.DelEntity(forwardEntity);
            }
            
            foreach (var backEntity in _backFilter)
            {
                FinishTask();
                _ecsWorld.DelEntity(backEntity);
            }
        }

        private void StartTask()
        {
            if (_npcFilter.IsEmpty)
                return;

            var npcEntity = _npcFilter.GetFirstEntity();

            if (_isUnderDOTweenAnimationTagPool.HasComponent(npcEntity))
                return;

            ref var waypoint = ref _waypointPool.GetComponent(npcEntity);
            ref var transformMdl = ref _transformMdlPool.GetComponent(npcEntity);
            ref var audioCmp= ref _audioClipPool.GetComponent(npcEntity);
            
            _isUnderDOTweenAnimationTagPool.AddComponent(npcEntity);

            var audioClip = audioCmp.audioClip;
            var transform = transformMdl.transform;
            var nextWaypoint = waypoint.Next();
            AudioSource audioSource;
            
            transform.DOLookAt(nextWaypoint.position, 1f).OnComplete(() =>
            {
                transform.DOScale(Vector3.one, 1)
                    .SetEase(Ease.InQuad)
                    .OnComplete(() =>
                    {
                        transform
                            .DOJump(nextWaypoint.position, 2, 7, 2.2f)
                            .SetEase(Ease.Linear)
                            .SetLink(transform.gameObject)
                            .OnComplete(() =>
                            {
                                if (_taskHolderMdlFilter.IsEmpty)
                                {
                                    _isUnderDOTweenAnimationTagPool.DelComponent(npcEntity);
                                    return;
                                }

                                ref var taskMdl =
                                    ref _taskHolderMdlPool.GetComponent(_taskHolderMdlFilter.GetFirstEntity());
                                ref var dialogMdl = ref _dialogMdlPool.GetComponent(npcEntity);

                                dialogMdl.image.gameObject.SetActive(true);
                                dialogMdl.text.gameObject.SetActive(true);

                                var textField = dialogMdl.text;
                                var taskQuest = dialogMdl.taskText;
                                var description = taskMdl.currentTask.Description;
                                audioSource = _soundService.PlaySound(audioClip);
                                DOTween
                                    .To(() => taskQuest, text => taskQuest = text, description,
                                        description.Length * 0.08f)
                                    .SetEase(Ease.Linear)
                                    .OnUpdate(() => textField.SetText(taskQuest))
                                    .OnComplete(() =>
                                    {
                                        _isUnderDOTweenAnimationTagPool.DelComponent(npcEntity);
                                        _soundService.StopSound(audioSource);
                                    });
                            });
                    });

            });
        }
        
        private void FinishTask()
        {
            if(_npcFilter.IsEmpty)
                return;
            
            var npcEntity = _npcFilter.GetFirstEntity();
            
            if(_isUnderDOTweenAnimationTagPool.HasComponent(npcEntity))
                return;
            
            ref var waypoint = ref _waypointPool.GetComponent(npcEntity);
            ref var transformMdl = ref _transformMdlPool.GetComponent(npcEntity);
            
            _isUnderDOTweenAnimationTagPool.AddComponent(npcEntity);
            
            ref var dialogMdl = ref _dialogMdlPool.GetComponent(npcEntity);

            dialogMdl.text.text = "";
            dialogMdl.taskText = "";
            
            dialogMdl.image.gameObject.SetActive(false);
            dialogMdl.text.gameObject.SetActive(false);

            var transform = transformMdl.transform;
            var firstPosition = waypoint.Next();
            var secondPosition = waypoint.Next();

            transform.DOLookAt(firstPosition.position, 0.7f).OnComplete(() =>
            {
                transform.DOJump(firstPosition.position, 2, 7, 2.2f)
                    .SetEase(Ease.Linear)
                    .SetLink(transform.gameObject)
                    .OnComplete(() =>
                    {
                        transform.DOScale(Vector3.zero, 1)
                            .SetEase(Ease.InQuad)
                            .OnComplete(() =>
                            {
                                transform.position = secondPosition.position;
                                _isUnderDOTweenAnimationTagPool.DelComponent(npcEntity);

                                var newEntity = _ecsWorld.NewEntity();
                                _forwardNpcEventPool.AddComponent(newEntity);
                            });
                    });
            });
        }
    }
}