using NaughtyAttributes;
using Secs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ingame
{
	public sealed class ShopBaker : MonoBehaviour
	{
		[Required, SerializeField] private Button refreshButton;
		[Required, SerializeField] private TMP_Text refreshText;
		[Required, SerializeField] private CardBaker cardPrefab;
		[SerializeField] private Transform[] slotTransform;
		
		private EcsWorld _world;
		
		[Inject]
		private void Construct(EcsWorldsProvider worldsProvider, ShopConfig shopConfig)
		{
			refreshButton.onClick.AddListener(OnRefreshButtonClicked);
			refreshText.SetText($"Refresh: {shopConfig.RefreshCost}");
			
			_world = worldsProvider.GameplayWorld;
			int entity = _world.NewEntity();
			
			_world.GetPool<ShopCmp>().AddComponent(entity) = new ShopCmp
			{
				cardPrefab = cardPrefab,
				slotsPositions = slotTransform
			};
		}

		private void OnRefreshButtonClicked()
		{
			_world.GetPool<RefreshShopEvent>().AddComponent(_world.NewEntity());
		}

		private void OnDrawGizmos()
		{
			if(slotTransform == null)
				return;
			
			foreach(var slotPosition in slotTransform)
			{
				Gizmos.color = Color.blue;
				Gizmos.DrawSphere(slotPosition.position, 1f);
			}
		}
	}
}