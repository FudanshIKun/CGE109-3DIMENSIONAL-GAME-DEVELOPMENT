using Unity.Collections;
using Unity.Jobs;

namespace Wonderland.GamePlay.BeatRunner
{
    public interface IPlayerBehavior
    {
        public void EnterBehavior();
        public void UpdateBehavior();
        public void LateUpdateBehavior();
    }
}
