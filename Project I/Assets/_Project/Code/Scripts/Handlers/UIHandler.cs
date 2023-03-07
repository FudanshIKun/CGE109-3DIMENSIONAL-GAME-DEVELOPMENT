using UnityEngine;

namespace Wonderland
{
    public class UIHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            MainManager.Instance.gameManager.currentUIHandler = this;
        }
        
        private void OnDisable()
        {
            MainManager.Instance.gameManager.currentUIHandler = null;
        }
    }
}
