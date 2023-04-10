using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Wonderland.Management
{
    public class GameManager : IManager
    {
        [Header("Scene Setting")]
        public Scene scene;
        private static GameState CurrentGameState { get; set; }
        private static IdleState IdleState { get; set; }
        private static LoadState LoadState { get; set; }
        public static event Action LoadNewScene;

        #region GameState Methods

        public static void ChangeGameState(State state)
        {
            switch (state)
            {
                case State.IdleState :
                    IdleState = new IdleState();
                    break;
                case State.LoadState :
                    LoadState = new LoadState();
                    break;
            }

            CurrentGameState = IdleState;
            CurrentGameState.EnterState();
        }

        public static bool CheckGameState(State state)
        {
            switch (state)
            {
                case State.IdleState :
                    if (CurrentGameState == IdleState) return true;
                    break;
                case State.LoadState :
                    if (CurrentGameState == LoadState) return true;
                    break;
            }

            return false;
        }

        #endregion

        #region SceneManagement Methods
        
        public async void LoadSceneAsync(Scene newScene)
        {
            if (newScene == Scene.None){return;}

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
        
        public async void LoadSceneWithLoaderAsync(Scene newScene)
        {
            if (newScene == Scene.None){return;}
            
            // Load newScene
            AsyncOperation load = SceneManager.LoadSceneAsync(newScene.ToString());
            load.allowSceneActivation = false;
            
            // Show Loading UI
            MainManager.Instance.UIManager.ShowLoadingScreen();
            LoadNewScene?.Invoke();

            do {
                await Task.Delay(100);
            } while (load.progress < 0.9f);

            await Task.Delay(1500);
            
            load.allowSceneActivation = true;
            await Task.Delay(1500);
        }
        #endregion

        private void UpdateInstance()
        {
            Logging.ManagerLogger.Log("GameManager Instance Updated");
            MainManager.Instance.GameManager.scene = scene;
        }

        private void Awake()
        {
            MainManager.OnDestroyMainManager += UpdateInstance;
            ChangeGameState(State.IdleState);
        }

        private void OnDisable()
        {
            MainManager.OnDestroyMainManager -= UpdateInstance;
        }
    }
}