using Ingame.Recipe;
using Ingame.Tasks;
using Secs;
using Secs.Physics;
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
			GeneralCardsConfig generalCardsConfig,
			ShopConfig shopConfig
		)
		{
			_world = ecsWorldsProvider.GameplayWorld;
			_ecsSystems = new EcsSystems(_world);
			_ecsSystems.AttachPhysics();
			
			_ecsSystems
				//Recipe
				.Add(new InitRecipesSys())
				.Add(new UnlockNewRecipeSys())
				.Add(new UnlockNewItemSys())
				//Tasks
				.Add(new CreateNewTaskSys())
				.Add(new CheckOfferedTaskItemValidationSys())
				//Shop
				.Add(new RefreshShopSystem(shopConfig, diContainer))
				//Cards
				.Add(new UpdateCardsViewSystem())
				.Add(new SelectCardSystem(inputService))
				.Add(new MoveCardSystem(inputService, generalCardsConfig))
				.Add(new DropCardSystem(inputService))
				.Add(new DisposeOnTickPhysicsSys());

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
			_ecsSystems.ReleasePhysics();
		}
	}
}