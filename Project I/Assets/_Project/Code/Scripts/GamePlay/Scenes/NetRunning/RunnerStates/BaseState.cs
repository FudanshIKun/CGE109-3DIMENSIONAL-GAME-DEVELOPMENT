namespace Wonderland.GamePlay.NetRunning
{
    public abstract class BaseState
    {
        public abstract void EnterState(Runner cat);
        
        public abstract void FixedUpdateState(Runner cat);
        
        public abstract void UpdateState(Runner cat);
    }
}
