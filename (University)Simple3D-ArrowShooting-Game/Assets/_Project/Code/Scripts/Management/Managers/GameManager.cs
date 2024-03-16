using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Wonderland.Management
{
    public class GameManager : SerializedMonoBehaviour
    {
        public enum State { IdleState, LoadState, PlayState }
        public static GameManager Instance { get; private set; }
        
        private static GameState CurrentGameState { get; set; }
        private static IdleState IdleState { get; set; }
        private static LoadState LoadState { get; set; }
        private static PlayState PlayState { get; set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
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
                    if (CurrentGameState == PlayState) return true;
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
                    IdleState ??= new IdleState();
                    CurrentGameState = IdleState;
                        break;
                case State.LoadState :
                    LoadState ??= new LoadState();
                    CurrentGameState = LoadState;
                    break;
                case State.PlayState:
                    PlayState ??= new PlayState();
                    CurrentGameState = PlayState;
                    break;
            }
            
            CurrentGameState.EnterState();
        }

        #endregion
    }
}