using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Ingame
{
	public sealed class UiCollectionViewBaker : MonoBehaviour
	{
		[Required, SerializeField] private UiCollectionView uiCollectionView;
		
		[Inject]
		private void Construct(EcsWorldsProvider worldsProvider)
		{
			var world = worldsProvider.GameplayWorld;
			int entity = world.NewEntity();
			
			world.GetPool<UiCollectionViewMdl>().AddComponent(entity).uiCollectionView = uiCollectionView;
			
			Destroy(this);
		}
	}
}