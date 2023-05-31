using Secs;
using Zenject;

namespace Ingame
{
	public sealed class GameplaySceneInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			InstallEcs();
		}

		private void InstallEcs()
		{
			var gameplayEcsWorld = new EcsWorld("gameplay");
			var ecsWorldsProvider = new EcsWorldsProvider(gameplayEcsWorld);

			Container
				.BindInstance(ecsWorldsProvider)
				.AsSingle();
		}
	}
}