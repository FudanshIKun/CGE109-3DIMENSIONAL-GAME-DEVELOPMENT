using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Wonderland
{
    public class GameManager : IManager
    {

        #region Scene Management

        [Header("Scene's Setting")] 
        public SceneHandler currentSceneHandler;
        public UIHandler currentUIHandler;
        public SceneSetting setting;
        public SceneType currentscene;
        public static SceneType CurrentScene;
        
        public static event Action LoadNewScene;

        #region Methods

        // ReSharper disable Unity.PerformanceAnalysis
        public async void LoadSceneAsync(SceneType newScene)
        {
            if (newScene == SceneType.None){return;}

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
            await Task.Delay(1500);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public async void LoadSceneWithLoaderAsync(SceneType newScene)
        {
            if (newScene == SceneType.None){return;}
            
            // Load newScene
            AsyncOperation load = SceneManager.LoadSceneAsync(newScene.ToString());
            load.allowSceneActivation = false;
            
            // Show Loading UI
            MainManager.Instance.uiManager.ShowLoadingScreen();
            LoadNewScene?.Invoke();

            do {
                await Task.Delay(100);
            } while (load.progress < 0.9f);

            await Task.Delay(1500);
            
            load.allowSceneActivation = true;
            await Task.Delay(1500);
        }

        #endregion

        #endregion
    }
}
