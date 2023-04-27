using System;
using UnityEngine;
using UnityEngine.Playables;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public class SceneHandler : Management.SceneHandler
    {
        public static SceneHandler Instance { get; private set; }
        [Header("Status")] 
        public bool gameHasStart;
        [Header("Settings")]
        public PlayableDirector timeline;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            SetUpScene();
        }

        #region Methods

        public override void SetUpScene()
        {
            try
            {
                Logging.HandlerLogger.Log("SetUpScene");
            }
            catch (Exception e)
            {
                Logging.HandlerLogger.Log("SetUpScene Has Been Canceled : " + e);
                throw;
            }
        }

        public override void LaunchGame()
        {
            Logging.HandlerLogger.Log("LaunchingGame");
            try
            {
                var ready = GameplayHandler.Instance.SetUpGameplay();
                if (ready)
                {
                    GameManager.ChangeGameState(Manager.State.PlayState);
                }
            }
            catch (Exception e)
            {
                Logging.HandlerLogger.Log("Launching Game Has Been Canceled : " + e);
                throw;
            }
        }

        #endregion
        
        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 70, 300, 30), "Launching Game "))
            {
                LaunchGame();
            }
        }
    }
}