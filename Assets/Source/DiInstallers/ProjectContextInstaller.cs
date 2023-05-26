using NaughtyAttributes;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame
{
	public sealed class ProjectContextInstaller : MonoInstaller
	{
		[Required, SerializeField] private SceneService sceneService;
		
		public override void InstallBindings()
		{
			InstallEcs();
			InstallSaveLoadService();
			InstallSceneService();
		}

		private void InstallEcs()
		{
			var gameplayEcsWorld = new EcsWorld();
			var ecsWorldsProvider = new EcsWorldsProvider(gameplayEcsWorld);

			Container
				.BindInstance(ecsWorldsProvider)
				.AsSingle();
		}
		
		private void InstallSaveLoadService()
		{
			Container
				.BindInterfacesAndSelfTo<SaveLoadService>()
				.AsSingle();
		}
		
		private void InstallSceneService()
		{
			Container
				.BindInstance(sceneService)
				.AsSingle();
		}
	}
}