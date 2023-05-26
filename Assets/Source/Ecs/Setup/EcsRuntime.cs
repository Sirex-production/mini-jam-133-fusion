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
		private void Construct(EcsWorldsProvider ecsWorldsProvider)
		{
			_world = ecsWorldsProvider.GameplayWorld;
			_ecsSystems = new EcsSystems(_world);
			
			SetupSystems();
		}
		
		private void SetupSystems()
		{
			
			_ecsSystems
				//Recipes 
				.Add(new TryToCombineSys())
				.Add(new UnlockNewRecipeSys())
				.Add(new InitRecipesSys())
				//Tasks
				.Add(new CreateNewTaskSys())
				//shop
				.Add(new CheckOfferedTaskItemValidationSys())
				.Add(new BuyItemSys());
		 
			_ecsSystems.AttachProfiler();
	
			_ecsSystems.Inject();
		}

		private void Start()
		{
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