namespace Wonderland.Client.MainWorld
{
    public class NormalBehavior : IPlayerBehavior
    {
        #region Systems

        private GameplayHandler GameplayHandler { get;}

        #endregion

        public NormalBehavior(GameplayHandler gameplayHandler, PlayerWeapon weapon)
        {
            GameplayHandler = gameplayHandler;
            GameplayHandler.EnableWeapon(weapon);
        }
        
        public void EnterBehavior()
        {
            CustomLog.GamePlaySystem.Log("Player Enter Behavior: " + this);
        } 

        public void UpdateBehavior()
        {
            GameplayHandler.Movement();
            GameplayHandler.Detect();
            GameplayHandler.Aim();
        }

        public void LateUpdateBehavior()
        {
            
        }
    }
}
