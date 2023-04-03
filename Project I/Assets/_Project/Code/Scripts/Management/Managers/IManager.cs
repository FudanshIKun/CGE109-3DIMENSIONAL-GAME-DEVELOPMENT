using UnityEngine;

namespace Wonderland.Management
{
    public class IManager : MonoBehaviour
    {
        public enum Scene
        {
            None,
            Authentication,
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
