using Unity.Collections;
using Unity.Jobs;

namespace Wonderland.Scene.MainWorld
{
    public interface IPlayerBehavior
    {
        public void EnterBehavior();
        public void UpdateBehavior();
        public void LateUpdateBehavior();
    }
}
