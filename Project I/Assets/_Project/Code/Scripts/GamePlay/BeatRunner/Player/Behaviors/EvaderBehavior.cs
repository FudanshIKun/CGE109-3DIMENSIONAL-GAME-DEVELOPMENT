using UnityEngine;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public class EvaderBehavior : IPlayerBehavior
    {
        #region Systems

        private MovementSystem MovementSystem { get;}
        private AimSystem AimSystem { get;}
        private WeaponSystem WeaponSystem { get; }

        #endregion

        public EvaderBehavior(MovementSystem movementSystem, AimSystem aimSystem, WeaponSystem weaponSystem)
        {
            MovementSystem = movementSystem;
            AimSystem = aimSystem;
            WeaponSystem = weaponSystem;
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
    }
}
