using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Ingame
{
	public sealed class UiVictoryViewBaker : MonoBehaviour
	{
		[Required, SerializeField] private UiVictoryView uiVictoryView;
		
		[Inject]
		private void Construct(EcsWorldsProvider worldsProvider)
		{
			var world = worldsProvider.GameplayWorld;
			int entity = world.NewEntity();
			
			world.GetPool<UiVictoryViewMdl>().AddComponent(entity).uiVictoryView = uiVictoryView;
			
			Destroy(this);
		}
	}
}