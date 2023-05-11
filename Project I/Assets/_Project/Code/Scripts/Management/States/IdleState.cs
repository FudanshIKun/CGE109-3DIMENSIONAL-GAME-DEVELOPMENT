namespace Wonderland.Management
{
    public class IdleState : GameState
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
