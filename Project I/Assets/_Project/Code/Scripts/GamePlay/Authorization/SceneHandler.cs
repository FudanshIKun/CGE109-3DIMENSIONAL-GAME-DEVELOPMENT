using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Wonderland.GamePlay.Authorization
{
    public class SceneHandler : Management.SceneHandler
    {
        #region Methods

        public override Task SetUpScene()
        {
            void Action()                    
            {                                
                                 
            }                                
                                 
            var setUpTask = new Task(Action);
            return setUpTask;                
        }

        #endregion
        
        public override async void LaunchingGame()
        {
            await SetUpScene();
            
        }

        protected override void OnEnable()
        {
            
        }

        private void Start()
        {
            LaunchingGame();
        }

        protected override void OnDisable()
        {
            
        }
    }
}
