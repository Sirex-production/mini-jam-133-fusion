using Ingame.Recipe;
using NaughtyAttributes;
using Secs;
using UnityEngine;
using Zenject;

namespace Ingame
{
	public sealed class ProjectContextInstaller : MonoInstaller
	{
		[Required, SerializeField] private GeneralCardsConfig generalCardsConfig;
		[Required, SerializeField] private ShopConfig shopConfig;
		[Required, SerializeField] private SceneService sceneService;
		[Required, SerializeField] private SoundService soundService;
		[Required, SerializeField] private AllRecipeContainerConfig allRecipeContainerConfig;
		
		public override void InstallBindings()
		{
			InstallEcs();
			InstallSaveLoadService();
			InstallSceneService();
			InstallSoundService();
			InstallInputSystem();
			InstallConfigs();
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

		private void InstallInputSystem()
		{
			Container
				.Bind<InputService>()
				.FromNew()
				.AsSingle()
				.NonLazy();
		}

		private void InstallSoundService()
		{
			Container.Bind<SoundService>()
				.FromInstance(soundService)
				.AsSingle()
				.NonLazy();
		}
		
		private void InstallConfigs()
		{
			Container
				.Bind<GeneralCardsConfig>()
				.FromInstance(generalCardsConfig)
				.AsSingle()
				.NonLazy();
			
			Container
				.Bind<ShopConfig>()
				.FromInstance(shopConfig)
				.AsSingle()
				.NonLazy();
			
			Container
				.Bind<AllRecipeContainerConfig>()
				.FromInstance(allRecipeContainerConfig)
				.AsSingle()
				.NonLazy();
		}
	}
}