using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using Wonderland.Management;

namespace Wonderland.Client.MainWorld
{
    public class SceneHandler : Client.SceneHandler
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

        public void SetUpScene()
        {
            try
            {
                CustomLog.Handler.Log("SetUpScene");
                InputHandler.Disable();
            }
            catch (Exception e)
            {
                CustomLog.Handler.Log("SetUpScene Has Been Canceled : " + e);
                throw;
            }
        }

        public void LaunchGame()
        {
            CustomLog.Handler.Log("LaunchGame");
            try
            {
                var ready = GameplayHandler.Instance.SetUpGameplay();
                if (ready)
                {
                    GameManager.ChangeGameState(GameManager.State.PlayState);
                }
            }
            catch (Exception e)
            {
                CustomLog.Handler.Log("Launching Game Has Been Canceled : " + e);
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