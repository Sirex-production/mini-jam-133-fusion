using Secs;
using UnityEngine;

namespace Ingame.Audio
{
	public sealed class SellItemSoundBaker : EcsMonoBaker
	{
		[SerializeField] private AudioClip audioClip;

		protected override void Bake(EcsWorld world, int entityId)
		{
			world.GetPool<AudioCmp>().AddComponent(entityId).audioClip = audioClip;
			world.GetPool<SellItemSoundTag>().AddComponent(entityId);
		}
	}
}