using System.Collections.Generic;
using Ingame.Audio;
using Ingame.Npc;
using Ingame.Recipe;
using Secs;
using UnityEngine;

namespace Ingame.Tasks
{
    public sealed class CheckOfferedTaskItemValidationSys : IEcsRunSystem
    {
        [EcsInject]
        private readonly EcsWorld _world;
        
        [EcsInject( typeof(TaskHolderMdl))]
        private readonly EcsFilter _taskFilter;
        
        [EcsInject( typeof(OfferTaskItemEvent))]
        private readonly EcsFilter _offerTaskItemEventFilter;

        [EcsInject( typeof(OfferedTaskItemsCmp))]
        private readonly EcsFilter _offeredItemsCmpFilter;
        
        [EcsInject( typeof(PlayerWalletCmp))]
        private readonly EcsFilter _walletCmpFilter;
        
        [EcsInject( typeof(IsUnderDOTweenAnimationTag),typeof(TaskNpcTag))]
        private readonly EcsFilter _npcFilter;
        
        [EcsInject( typeof(TaskCompletedSoundTag),typeof(AudioCmp))]
        private readonly EcsFilter _audioFilter;
        
        [EcsInject]
        private readonly EcsPool<TaskHolderMdl> _taskPool;
        
        [EcsInject]
        private readonly EcsPool<OfferedTaskItemsCmp> _offerItemsPool;
        
        [EcsInject]
        private readonly EcsPool<AskNewTaskEvent> _askNewTaskPool;
        
        [EcsInject]
        private readonly EcsPool<UpdateCardsViewEvent> _updateCardsViewEvenPool;
        
        [EcsInject]
        private readonly EcsPool<PlayerWalletCmp> _walletCmpPool;
        
        [EcsInject]
        private readonly EcsPool<MoveBackNpcEvent> _moveBckNpcEventPool;
        
        [EcsInject]
        private readonly EcsPool<UpdateGameplayUiEvent> _updateGameplayUiEventPool;

        [EcsInject] 
        private readonly EcsPool<AudioCmp> _audioPool;
        
        private SoundService _soundService;

        public CheckOfferedTaskItemValidationSys(SoundService soundService)
        {
            _soundService = soundService;
        }
        
        public void OnRun()
        {
            foreach (var offerTaskItemEventEntity in _offerTaskItemEventFilter)
            {
                if (_offeredItemsCmpFilter.IsEmpty || !_npcFilter.IsEmpty || _taskFilter.IsEmpty || _walletCmpFilter.IsEmpty)
                {
                    _world.DelEntity(offerTaskItemEventEntity);
                    continue;
                }

                ref var offeredTaskItemsCmp = ref _offerItemsPool.GetComponent(_offeredItemsCmpFilter.GetFirstEntity());
                ref var taskHolderMdl = ref _taskPool.GetComponent(_taskFilter.GetFirstEntity());
                ref var walletCmp = ref _walletCmpPool.GetComponent(_walletCmpFilter.GetFirstEntity());
                
                if (offeredTaskItemsCmp.IsTradeAccepted(new List<ItemConfig>(taskHolderMdl.currentTask.QuestItems)))
                {
                    if (!_audioFilter.IsEmpty)
                    {
                       ref var audioCmp = ref _audioPool.GetComponent(_audioFilter.GetFirstEntity());
                       
                       if(audioCmp.audioSource != null)
                           _soundService.StopSound(audioCmp.audioSource);

                       audioCmp.audioSource = _soundService.PlaySound(audioCmp.audioClip);
                    }
                    
                    walletCmp.currentAmountOfCoins += taskHolderMdl.currentTask.Money;
                    
                    offeredTaskItemsCmp.SubtractItems(new List<ItemConfig>(taskHolderMdl.currentTask.QuestItems));
                    
                    var newEntity = _world.NewEntity();
                    _askNewTaskPool.AddComponent(newEntity);
                    
                    newEntity = _world.NewEntity();
                    _updateCardsViewEvenPool.AddComponent(newEntity);
                    
                    newEntity = _world.NewEntity();
                    _moveBckNpcEventPool.AddComponent(newEntity);
                    
                    newEntity = _world.NewEntity();
                    _updateGameplayUiEventPool.AddComponent(newEntity);
                    
                }
                
                _world.DelEntity(offerTaskItemEventEntity);
            }
        }
    }
}