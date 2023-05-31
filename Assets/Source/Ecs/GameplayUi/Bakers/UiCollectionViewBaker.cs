using NaughtyAttributes;
using Secs;
using UnityEngine;

namespace Ingame
{
	public sealed class UiCollectionViewBaker : EcsMonoBaker
	{
		[Required, SerializeField] private UiCollectionView uiCollectionView;

		protected override void Bake(EcsWorld world, int entityId)
		{
			world.GetPool<UiCollectionViewMdl>().AddComponent(entityId).uiCollectionView = uiCollectionView;
		}
	}
}