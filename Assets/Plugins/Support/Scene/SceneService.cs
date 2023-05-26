
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Support
{
    public sealed class SceneService : MonoBehaviour
    {
        private const float DELAY_BEFORE_LOADING= 1f;
        private const float DELAY_BEFORE_FINISHING= 1f;
        
        public event Action OnLoadingStarted;
        public event Action<float> OnLoadingInProgress;
        public event Action OnLoadingDone;

        private Coroutine _levelCoroutine;
        public void LoadLevel(int level)
        {
            if (level < 0 || level >= SceneManager.sceneCountInBuildSettings)
            {
                Debug.LogWarning("Level can not be smaller than zero or bigger than number of scenes");
                return;
            }
            
            if(_levelCoroutine != null)
                return;

            _levelCoroutine = StartCoroutine(LoadSceneCoroutine(level));
          
        }

        public void ResetLevel()
        {
            LoadLevel(SceneManager.GetActiveScene().buildIndex);
        }

        private IEnumerator LoadSceneCoroutine(int level)
        {
            OnLoadingStarted?.Invoke();

            yield return new WaitForSeconds(DELAY_BEFORE_LOADING);
            
            var loading = SceneManager.LoadSceneAsync(level);

            while (!loading.isDone)
            {
                OnLoadingInProgress?.Invoke(loading.progress);
                yield return null;
            }

            yield return new WaitForSeconds(DELAY_BEFORE_FINISHING);

            _levelCoroutine = null;
            OnLoadingDone?.Invoke();
        }
    }
}