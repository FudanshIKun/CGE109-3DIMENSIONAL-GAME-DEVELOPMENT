using UnityEngine;

namespace Wonderland.Management
{
    public class GameSceneHandler : SceneHandler
    {
        private void OnEnable()
        {
            MainManager.GameSceneHandler = this;
        }
        
        private void OnDisable()
        {
            MainManager.GameSceneHandler = null;
        }
    }
}
