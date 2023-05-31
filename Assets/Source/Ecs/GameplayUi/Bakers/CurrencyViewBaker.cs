using NaughtyAttributes;
using Secs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ingame
{
	public sealed class CurrencyViewBaker : EcsMonoBaker
	{
		[FormerlySerializedAs("currencyView")] [Required, SerializeField] private UiCurrencyView uiCurrencyView;
		
		protected override void Bake(EcsWorld world, int entityId)
		{
			world.GetPool<CurrencyViewMdl>().AddComponent(entityId).uiCurrencyView = uiCurrencyView;
		}
	}
}