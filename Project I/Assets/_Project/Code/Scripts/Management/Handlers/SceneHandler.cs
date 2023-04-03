using UnityEngine;

namespace Wonderland.Management
{
    public class SceneHandler : MonoBehaviour
    {
        protected bool SceneReady = false;
        public SceneSetting sceneSetting;
        private void OnEnable()
        {
            MainManager.SceneHandler = this;
            MainManager.Setting = sceneSetting;
        }
        
        private void OnDisable()
        {
            MainManager.SceneHandler = null;
            MainManager.Setting = null;
        }
    }
}
