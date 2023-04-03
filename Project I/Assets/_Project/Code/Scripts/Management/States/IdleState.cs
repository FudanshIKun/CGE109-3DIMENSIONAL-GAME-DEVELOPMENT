namespace Wonderland.Management
{
    public class IdleState : GameState
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
