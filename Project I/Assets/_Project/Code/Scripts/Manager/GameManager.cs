using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wonderland.GamePlay;

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
                Instance.Setting = Setting;
                Destroy(gameObject);
            }
        }

        #endregion

        #region Scene Management
        
        [Header("Scene's Setting")]
        public SceneSetting Setting;
        
        public static event Action LoadNewScene;
        
        public enum SceneType
        {
            Authentication,
            Lobby,
            DailyMiniGame
        }

        public SceneType currentScene;

        #region Methods

        /// <summary>
        /// This method is used to load new scene with specific orientation and without Loading transition
        /// </summary>
        /// <param name="new Scene, ScreenOrientation"></param>
        public async void LoadSceneAsync(SceneType newScene)
        {
            // Load newScene
            AsyncOperation load = SceneManager.LoadSceneAsync(newScene.ToString());
            load.allowSceneActivation = false;
            LoadNewScene?.Invoke();
            
            do
            {
                await Task.Delay(100);
            } while (load.progress < 0.9f);

            await Task.Delay(1500);
            
            load.allowSceneActivation = true;
        }

        /// <summary>
        /// This method is used to load new scene with specific orientation and with Loading transition
        /// </summary>
        /// <param name="new Scene, ScreenOrientation"></param>
        public async void LoadSceneWithLoaderAsync(SceneType newScene)
        {
            // Load newScene
            AsyncOperation load = SceneManager.LoadSceneAsync(newScene.ToString());
            load.allowSceneActivation = false;
            
            // Show Loading UI
            UIManager.Instance.ShowLoadingScreen();
            LoadNewScene?.Invoke();

            do {
                await Task.Delay(100);
            } while (load.progress < 0.9f);

            await Task.Delay(1500);
            
            load.allowSceneActivation = true;
            UIManager.Instance.HideLoadingScreen();
        }

        #endregion

        #endregion

        public void Awake()
        {
            Logging.LoadLogger();
            Singleton();
        }
    }
}
