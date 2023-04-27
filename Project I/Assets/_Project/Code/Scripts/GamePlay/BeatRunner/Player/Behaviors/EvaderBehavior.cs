namespace Wonderland.GamePlay.BeatRunner
{
    public class EvaderBehavior : IPlayerBehavior
    {
        #region Systems

        private MovementSystem MovementSystem { get;}
        private AimSystem AimSystem { get;}

        #endregion

        public EvaderBehavior(MovementSystem movementSystem, AimSystem aimSystem, WeaponSystem weaponSystem)
        {
            MovementSystem = movementSystem;
            AimSystem = aimSystem;
            aimSystem.weaponSystem = weaponSystem;
        }
        
        public void EnterBehavior()
        {
            Logging.GamePlaySystemLogger.Log("Player Enter Behavior: " + this);
        } 

        public void UpdateBehavior()
        {
            MovementSystem.MoveAndTurn();
            AimSystem.Aim();

        }

        public void LateUpdateBehavior()
        {
            
        }
    }
}
