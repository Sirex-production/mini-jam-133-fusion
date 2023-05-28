using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Ingame
{
	public sealed class CurrencyViewBaker : MonoBehaviour
	{
		[FormerlySerializedAs("currencyView")] [Required, SerializeField] private UiCurrencyView uiCurrencyView;
		
		[Inject]
		private void Construct(EcsWorldsProvider worldsProvider)
		{
			var world = worldsProvider.GameplayWorld;
			int entity = world.NewEntity();

			world.GetPool<CurrencyViewMdl>().AddComponent(entity).uiCurrencyView = uiCurrencyView;
			
			Destroy(this);
		}
	}
}