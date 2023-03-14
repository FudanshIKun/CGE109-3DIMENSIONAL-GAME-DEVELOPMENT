using UnityEngine;

namespace Wonderland
{
    public class SceneHandler : MonoBehaviour
    {
        public SceneSetting sceneInfo;
        private void OnEnable()
        {
            MainManager.Instance.sceneHandler = this;
        }
        
        private void OnDisable()
        {
            MainManager.Instance.sceneHandler = null;
        }
    }
}
