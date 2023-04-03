using System;
using UnityEngine;
using UnityEngine.Playables;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public class BeatRunnerHandler : SceneHandler
    {
        #region Handlers

        public static UIHandler MainUIHandler;

        #endregion

        #region Scene Fields

        public static PlayableDirector MainDirector;

        #endregion

        #region Methods

        private void ReadyScene()
        {
            GameManager.ChangeGameState(IManager.State.IdleState);
            SceneReady = true;
        }

        private void LaunchingGame()
        {
            if (SceneReady)
            {
                GameManager.ChangeGameState(IManager.State.PlayState);
            }
        }

        #endregion

        private void Awake()
        {
            ReadyScene();
        }

        private void OnEnable()
        {
            
        }

        private void Start()
        {
            
        }
    }
}
