using NaughtyAttributes;
using Secs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ingame
{
	public sealed class ShopBaker : EcsMonoBaker
	{
		[Required, SerializeField] private Button refreshButton;
		[Required, SerializeField] private TMP_Text refreshText;
		[Required, SerializeField] private CardBaker cardPrefab;
		[SerializeField] private Transform[] slotTransform;
		
		private EcsWorld _world;
		
		[Inject]
		private void Construct(ShopConfig shopConfig)
		{
			refreshButton.onClick.AddListener(OnRefreshButtonClicked);
			refreshText.SetText($"Refresh: {shopConfig.RefreshCost}");
		}

		protected override void Bake(EcsWorld world, int entityId)
		{
			_world = world;
			
			world.GetPool<ShopCmp>().AddComponent(entityId) = new ShopCmp
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