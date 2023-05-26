using Ingame.Receipt;
using Ingame.Tasks;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame
{
	public sealed class EcsRuntime : MonoBehaviour
	{
		private EcsSystems _ecsSystems;
		
		[Inject]
		private void Construct(EcsWorldsProvider ecsWorldsProvider, InputService inputService)
		{
			_ecsSystems = new EcsSystems(ecsWorldsProvider.GameplayWorld);
			
			_ecsSystems
				.Add(new UnlockNewReceiptsSys())
				.Add(new InitReceiptsSys())
				.Add(new CreateNewTaskSys())
				.Add(new CheckOfferedTaskItemValidationSys());
		 
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