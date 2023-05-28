using Ingame.Recipe;
using Ingame.Tasks;
using Secs;
using Ingame;
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
			AllRecipeContainerConfig allRecipeContainerConfig
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
				.Add(new CheckOfferedTaskItemValidationSys())
				//Shop
				.Add(new RefreshShopSystem(shopConfig, diContainer))
				.Add(new SellItemSystem())
				//Cards
				.Add(new UpdateCardsViewSystem())
				.Add(new SelectCardSystem(inputService))
				.Add(new MoveCardSystem(inputService, generalCardsConfig))
				.Add(new DropCardSystem(inputService))
				.Add(new MoveShopCardsBackToShopSystem())
				.Add(new UpdateCardMotionSystem(generalCardsConfig))
				//Fusion
				.Add(new MergeCardsSystem(allRecipeContainerConfig))
				//Camerawork
				.Add(new MoveCameraSystem(inputService, settingsService))
				//Gameplay UI
				.Add(new UpdateCurrencyViewSystem())
				.Add(new UpdateUiCollectionViewSystem())
				//NPC
				.Add(new MoveTaskNpcSys(soundService))
				//Physics
				.Add(new DisposeOnTickPhysicsSys())
				.Add(new FullyDestroyObject());

			_ecsSystems.AttachProfiler();
			_ecsSystems.Inject();
		}

		private void Start()
		{
			_world.UpdateFilters();
			_ecsSystems.FireInitSystems();
		}

		private void Update()
		{
			_ecsSystems.FireRunSystems();
		}

		private void OnDestroy()
		{
			_ecsSystems.FireDisposeSystems();
			_ecsSystems.ReleaseProfiler();
			EcsPhysics.UnbindToEcsWorld();
		}
	}
}