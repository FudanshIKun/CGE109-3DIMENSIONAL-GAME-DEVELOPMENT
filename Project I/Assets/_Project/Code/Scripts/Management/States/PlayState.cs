using UnityEngine;

namespace Wonderland.Management
{
    public class PlayState : GameState
    {
        public override void EnterState()
        {
            Logging.ManagerLogger.Log("Game Enter " + this);
        }

        public override void UpdateState()
        {
            
        }
    }
}
