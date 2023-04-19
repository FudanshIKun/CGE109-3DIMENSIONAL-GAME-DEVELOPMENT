using System;


namespace Wonderland.GamePlay.Authorization
{
    public class SceneHandler : Management.SceneHandler
    {
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

        #endregion
        
        public override void LaunchGame()
        {
            
            
        }

        private void Start()
        {
            SetUpScene();
        }
    }
}
