namespace Wonderland.GamePlay.BeatRunner
{
    public class NormalBehavior : IPlayerBehavior
    {
        #region Systems

        private MovementSystem MovementSystem { get;}
        private AimSystem AimSystem { get;}

        #endregion

        public NormalBehavior(MovementSystem movementSystem, AimSystem aimSystem, WeaponSystem weaponSystem)
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
            MovementSystem.InputMagnitude();
            MovementSystem.CheckGround();
            AimSystem.Aim();

        }

        public void LateUpdateBehavior()
        {
            
        }
    }
}
