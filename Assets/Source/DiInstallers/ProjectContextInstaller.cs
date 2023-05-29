using Ingame.Recipe;
using NaughtyAttributes;
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
		[Required, SerializeField] private DefaultSettingsConfig defaultSettingsConfig;
		[Required, SerializeField] private AllItemsConfig allItemsConfig;
		[Required, SerializeField] private SocialMediaConfig socialMediaConfig;
		
		public override void InstallBindings()
		{
			InstallSaveLoadService();
			InstallSceneService();
			InstallSoundService();
			InstallInputSystem();
			InstallSettingsService();
			InstallConfigs();
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

		private void InstallSettingsService()
		{
			Container.Bind<SettingsService>()
				.FromNew()
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
			
			Container
				.Bind<DefaultSettingsConfig>()
				.FromInstance(defaultSettingsConfig)
				.AsSingle()
				.NonLazy();
			
			Container
				.Bind<AllItemsConfig>()
				.FromInstance(allItemsConfig)
				.AsSingle()
				.NonLazy();
			
			Container
				.Bind<SocialMediaConfig>()
				.FromInstance(socialMediaConfig)
				.AsSingle()
				.NonLazy();
		}
	}
}