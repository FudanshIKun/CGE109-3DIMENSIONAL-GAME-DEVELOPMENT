using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Wonderland.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager Instance;

        private void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Instance.currentScene = currentScene;
                Destroy(gameObject);
            }
        }

        #endregion

        #region Scene Management

        public enum SceneType
        {
            Authentication,
            Lobby,
            DailyMiniGame
        }

        public SceneType currentScene;

        #region Methods

        public void LoadScene(SceneType newScene, ScreenOrientation type)
        {
            StartCoroutine(LoadSceneAsync(newScene, type));
        }

        public void LoadSceneWithLoaderTransition(SceneType newScene, ScreenOrientation type)
        {
            StartCoroutine(LoadSceneWithLoaderTransitionAsync(newScene, type));
        }

        /// <summary>
        /// This method is used to load new scene with specific orientation and without Loading transition
        /// </summary>
        /// <param name="new Scene, ScreenOrientation"></param>
        private IEnumerator LoadSceneAsync(SceneType newScene, ScreenOrientation type)
        {
            // Check if the newScene exists
            if (!SceneManager.GetSceneByName(newScene.ToString()).IsValid())
            {
                Debug.LogError("LoadNewScene encountered an error : The specified scene is not valid");
                yield break;
            }
            
            // Set Screen's orientation
            Screen.orientation = type;
            
            // Load newScene
            AsyncOperation load = SceneManager.LoadSceneAsync(newScene.ToString());
            while (!load.isDone)
            {
                yield return null;
            }
        }

        /// <summary>
        /// This method is used to load new scene with specific orientation and with Loading transition
        /// </summary>
        /// <param name="new Scene, ScreenOrientation"></param>
        private IEnumerator LoadSceneWithLoaderTransitionAsync(SceneType newScene, ScreenOrientation type)
        {
            // Check if the newScene exists
            if (!SceneManager.GetSceneByName(newScene.ToString()).IsValid())
            {
                Debug.LogError("LoadNewScene encountered an error : The specified scene is not valid");
                yield break;
            }
            
            // Set Screen's orientation
            Screen.orientation = type;
            
            // Load newScene
            AsyncOperation load = SceneManager.LoadSceneAsync(newScene.ToString());
            while (!load.isDone)
            {
                yield return null;
            }
        }

        #endregion

        #endregion

        public void Awake()
        {
            Singleton();
        }

        private void Start()
        {
            
        }
    }
}
