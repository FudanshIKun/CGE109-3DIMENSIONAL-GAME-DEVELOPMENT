using UnityEngine;

namespace Wonderland
{
    public class UIHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            MainManager.Instance.uiHandler = this;
        }
        
        private void OnDisable()
        {
            MainManager.Instance.uiHandler = null;
        }
    }
}
