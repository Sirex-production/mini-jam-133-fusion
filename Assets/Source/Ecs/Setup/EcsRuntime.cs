using Ingame.Recipe;
using Ingame.Shop;
using Ingame.Tasks;
using Secs;
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
			EcsWorldsProvider ecsWorldsProvider,
			InputService inputService,
			GeneralCardsConfig generalCardsConfig
		)
		{
			_world = ecsWorldsProvider.GameplayWorld;
			_ecsSystems = new EcsSystems(_world);

			_ecsSystems
				//Recipe
				.Add(new InitRecipesSys())
				.Add(new UnlockNewRecipeSys())
				.Add(new UnlockNewItemSys())
				//Tasks
				.Add(new CreateNewTaskSys())
				//shop
				.Add(new CheckOfferedTaskItemValidationSys())
				.Add(new BuyItemSys())
				//Cards
				.Add(new SelectCardSystem(inputService))
				.Add(new MoveCardSystem(inputService, generalCardsConfig))
				.Add(new DropCardSystem(inputService));
			

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
		}
	}
}