using System;
using UnityEngine;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public class GameplayHandler : Management.GameplayHandler
    {
        public static GameplayHandler Instance { get; private set; }
        public Player Player;
        
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

        private void OnEnable()
        {
            
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            #region Logging
            

            #endregion
        }

        #region Methods

        private bool CheckGameplay()
        {
            return true;
        }

        public override bool SetUpGameplay()
        {
            Logging.HandlerLogger.Log("SetUpGameplay");
            JoyStickDetection.Controller.gameObject.SetActive(true);

            return CheckGameplay();
        }

        #endregion
    }
}