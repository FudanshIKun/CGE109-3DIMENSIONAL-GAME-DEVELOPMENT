using UnityEngine;

namespace Wonderland
{
    public class SceneHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            MainManager.Instance.gameManager.currentSceneHandler = this;
        }
        
        private void OnDisable()
        {
            MainManager.Instance.gameManager.currentSceneHandler = null;
        }
    }
}
