using UnityEngine;
using Wonderland.Utility;

namespace Wonderland.GamePlay.KittyRun
{
    public class KittyFinishedState : KittyBaseState
    {
        public override void EnterState(KittyRunCat cat)
        {
            Logging.GamePlayLogger.Log("Cat Enter State : " + this);
        }
                
        public override void FixedUpdateState(KittyRunCat cat)
        {
                    
        }
        
        public override void UpdateState(KittyRunCat cat)
        {
                    
        }
    }
}
