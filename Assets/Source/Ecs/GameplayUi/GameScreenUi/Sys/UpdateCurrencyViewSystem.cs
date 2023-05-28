using Secs;
using UnityEngine;

namespace Ingame
{
	public sealed class UpdateCurrencyViewSystem : IEcsInitSystem, IEcsRunSystem
	{
		[EcsInject]
		private readonly EcsWorld _world;
		
		[EcsInject(typeof(CurrencyViewMdl))]
		private readonly EcsFilter _currencyViewFilter;
		[EcsInject(typeof(PlayerWalletCmp))]
		private readonly EcsFilter _playerWalletFilter;
		[EcsInject(typeof(UpdateGameplayUiEvent))]
		private readonly EcsFilter _updateGameplayUiEventFilter;
		
		[EcsInject]
		private readonly EcsPool<CurrencyViewMdl> _currencyViewPool;
		[EcsInject]
		private readonly EcsPool<PlayerWalletCmp> _playerWalletPool;
		[EcsInject]
		private readonly EcsPool<UpdateGameplayUiEvent> _updateGameplayUiEventPool;

		public void OnInit()
		{
			_updateGameplayUiEventPool.AddComponent(_world.NewEntity());
		}
		
		public void OnRun()
		{
			if(_updateGameplayUiEventFilter.IsEmpty)
				return;
			
			_updateGameplayUiEventPool.DelComponent(_updateGameplayUiEventFilter.GetFirstEntity());

			if(_playerWalletFilter.IsEmpty)
				return;

			ref var playerWalletCmp = ref _playerWalletPool.GetComponent(_playerWalletFilter.GetFirstEntity());
			
			foreach(var entity in _currencyViewFilter)
			{
				ref var currencyViewMdl = ref _currencyViewPool.GetComponent(entity);
				currencyViewMdl.uiCurrencyView.SetCurrency(playerWalletCmp.currentAmountOfCoins);
			}
		}
	}
}