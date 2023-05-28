using System;
using Zenject;

namespace Ingame
{
	public sealed class SettingsService
	{
		public SettingsData currentSettingsData;

		[Inject]
		public SettingsService(DefaultSettingsConfig settingsConfig)
		{
			currentSettingsData = settingsConfig.DefaultSettingsData;
		}
	}

	[Serializable]
	public struct SettingsData
	{
		public float soundVolume;
		public float mapMouseDraggingSpeed;
		public float cameraMovementSpeed;
	}
}