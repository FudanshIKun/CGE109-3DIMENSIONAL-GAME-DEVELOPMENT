using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Wonderland.Management
{
    public class GameManager : IManager
    {
        private static GameState CurrentGameState { get; set; }
        private static IdleState IdleState { get; set; }
        private static LoadState LoadState { get; set; }
        public static event Action LoadNewScene;
        
        private void Awake()
        {
            ChangeGameState(State.IdleState);
        }

        #region Methods

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
                case State.PlayState:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            return false;
        }

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
                case State.PlayState:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            CurrentGameState = IdleState;
            CurrentGameState.EnterState();
        }
        
        public static async void LoadSceneWithLoaderAsync(Scene newScene)
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
    }
}