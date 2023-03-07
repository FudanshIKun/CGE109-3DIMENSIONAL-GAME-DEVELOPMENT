using UnityEngine;

namespace Wonderland.GamePlay.NetRunning
{
    public class FinishedState : BaseState
    {
        public override void EnterState(Runner cat)
        {
            Logging.GamePlayLogger.Log("Cat Enter State : " + this);
        }
                
        public override void FixedUpdateState(Runner cat)
        {
                    
        }
        
        public override void UpdateState(Runner cat)
        {
                    
        }
    }
}
