using UnityEngine;

namespace Wonderland.Management
{
    public abstract class Manager : MonoBehaviour
    {
        public enum Scene
        {
            None,
            Authorization,
            BeatRunner
        }

        public enum State
        {
            IdleState,
            LoadState,
            PlayState
        }
        
        
    }
}
