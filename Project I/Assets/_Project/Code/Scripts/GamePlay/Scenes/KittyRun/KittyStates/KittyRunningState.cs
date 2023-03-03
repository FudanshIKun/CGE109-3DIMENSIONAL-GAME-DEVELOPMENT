using UnityEngine;
using Wonderland.Utility;

namespace Wonderland.GamePlay.KittyRun
{
    public class KittyRunningState : KittyBaseState
    {
        public override void EnterState(KittyRunCat cat)
        {
            Logging.GamePlayLogger.Log("Cat Enter State : " + this);
        }
                
        public override void FixedUpdateState(KittyRunCat cat)
        {
            var velocity = cat._rigidbody.velocity;
            velocity = new Vector3(velocity.x, velocity.y, cat.runVelocity);
            cat._rigidbody.velocity = velocity;
            Logging.GamePlayLogger.Log("Run : " +cat._rigidbody.velocity);
        }
        
        public override void UpdateState(KittyRunCat cat)
        {
                    
        }
    }
}
