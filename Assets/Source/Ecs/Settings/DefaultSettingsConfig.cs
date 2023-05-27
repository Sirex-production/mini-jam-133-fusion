using UnityEngine;

namespace Ingame
{
	[CreateAssetMenu(fileName = "DefaultSettingsConfig", menuName = "Ingame/DefaultSettingsConfig")]
	public sealed class DefaultSettingsConfig : ScriptableObject
	{
		[SerializeField] private SettingsData defaultSettingsData;

		public SettingsData DefaultSettingsData => defaultSettingsData;
	}
}