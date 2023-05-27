using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Ingame
{
	public sealed class ShopBaker : MonoBehaviour
	{
		[Required, SerializeField] private CardBaker cardPrefab;
		[SerializeField] private Transform[] slotTransform;
		
		[Inject]
		private void Construct(EcsWorldsProvider worldsProvider)
		{
			var world = worldsProvider.GameplayWorld;
			int entity = world.NewEntity();
			
			world.GetPool<ShopCmp>().AddComponent(entity) = new ShopCmp
			{
				cardPrefab = cardPrefab,
				slotsPositions = slotTransform
			};
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