﻿using Ingame.Recipe;
using Ingame.Tasks;
using Secs;
using Ingame.Npc;
using UnityEngine;
using Zenject;

namespace Ingame
{
	public sealed class EcsRuntime : MonoBehaviour
	{
		private EcsWorld _world;
		private EcsSystems _ecsSystems;

		[Inject]
		private void Construct
		(
			DiContainer diContainer,
			EcsWorldsProvider ecsWorldsProvider,
			InputService inputService,
			SettingsService settingsService,
			SoundService soundService,
			GeneralCardsConfig generalCardsConfig,
			ShopConfig shopConfig,
			AllRecipeContainerConfig allRecipeContainerConfig,
			AllItemsConfig allItemsConfig
		)
		{
			_world = ecsWorldsProvider.GameplayWorld;
			_ecsSystems = new EcsSystems(_world);
			
			EcsPhysics.BindToEcsWorld(_world, _ecsSystems);

			_ecsSystems
				//Recipe
				.Add(new InitRecipesSys())
				.Add(new UnlockNewRecipeSys())
				.Add(new UnlockNewItemSys())
				//Tasks
				.Add(new DetectCollisionWithTasksTable())
				.Add(new CreateNewTaskSys())
				.Add(new CheckOfferedTaskItemValidationSys(soundService))
				//Shop
				.Add(new RefreshShopSystem(shopConfig, diContainer, soundService))
				.Add(new SellItemSystem(soundService))
				//Cards
				.Add(new UpdateCardsViewSystem())
				.Add(new SelectCardSystem(inputService, soundService))
				.Add(new MoveCardSystem(inputService, generalCardsConfig))
				.Add(new DropCardSystem(inputService))
				.Add(new MoveShopCardsBackToShopSystem())
				.Add(new UpdateCardMotionSystem(generalCardsConfig))
				//Fusion
				.Add(new MergeCardsSystem(allRecipeContainerConfig, soundService))
				//Camerawork
				.Add(new MoveCameraSystem(inputService, settingsService))
				//Gameplay UI
				.Add(new UpdateCurrencyViewSystem())
				.Add(new UpdateUiCollectionViewSystem())
				.Add(new CheckWinConditionsSystem(allItemsConfig))
				//NPC
				.Add(new MoveTaskNpcSys(soundService))
				//Physics
				.Add(new DisposeOnTickPhysicsSys())
				.Add(new FullyDestroyObject());
#if UNITY_EDITOR
			_ecsSystems.AttachProfiler();
#endif
			_ecsSystems.Inject();
			_world.BakeAllBakersInScene();
		}
		
		private void Start()
		{
			// _world.UpdateFilters();
			_ecsSystems.FireInitSystems();
		}

		private void Update()
		{
			_ecsSystems.FireRunSystems();
		}

		private void OnDestroy()
		{
			_ecsSystems.FireDisposeSystems();
#if UNITY_EDITOR
			_ecsSystems.ReleaseProfiler();
#endif
			EcsPhysics.UnbindToEcsWorld();
		}
	}
}