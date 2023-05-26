using Secs;

namespace Ingame
{
	public sealed class EcsWorldsProvider
	{
		private readonly EcsWorld _gameplayWorld;
		
		public EcsWorld GameplayWorld => _gameplayWorld;

		public EcsWorldsProvider(EcsWorld gameplayWorld)
		{
			_gameplayWorld = gameplayWorld;
		}
	}
}