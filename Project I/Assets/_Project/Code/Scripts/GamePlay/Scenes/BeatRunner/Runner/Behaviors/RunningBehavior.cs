using UnityEngine;

namespace Wonderland.GamePlay.BeatRunner.Runner
{
    public class RunningBehavior : IRunnerBehavior
    {
        public Runner Runner { get; set; }
        public void FixedUpdateBehavior()
        {
            Runner.rigidbody.MovePosition(Runner.rigidbody.position + Vector3.forward * (Runner.moveSpeed * Time.fixedDeltaTime));
        }

        public void UpdateBehavior()
        {
            
        }

        public void LateUpdateBehavior()
        {
            
        }

        public void UpSwipe()
        {
            
        }

        public void LefSwipe()
        {
            
        }

        public void RightSwipe()
        {
            
        }

        public void DownSwipe()
        {
            
        }
    }
}
