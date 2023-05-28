using System;
using UnityEngine;
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
			AudioListener.volume = settingsConfig.DefaultSettingsData.soundVolume;
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