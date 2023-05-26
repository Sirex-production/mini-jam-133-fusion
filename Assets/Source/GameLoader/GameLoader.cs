using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Ingame
{
	public sealed class GameLoader : MonoBehaviour
	{
		[Scene, SerializeField] private int sceneToLoad;
		
		private SaveLoadService _saveLoadService;
		private SceneService _sceneService;
		
		[Inject]
		private void Construct(SaveLoadService saveLoadService, SceneService sceneService)
		{
			_saveLoadService = saveLoadService;
			_sceneService = sceneService;
			
			InitializeServices();
		}

		private void InitializeServices()
		{
			_saveLoadService.LoadSave();
			_sceneService.LoadLevel(sceneToLoad);
		}
	}
}