using UnityEngine;

namespace Wonderland.Management
{
    public class PlayState : GameState
    {
        public override void EnterState()
        {
            CustomLog.GameState.Log("Game Enter " + this);
        }

        public override void UpdateState()
        {
            
        }
    }
}
