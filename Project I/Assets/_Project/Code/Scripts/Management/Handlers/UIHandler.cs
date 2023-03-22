using UnityEngine;

namespace Wonderland.Management
{
    public class UIHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            MainManager.UIHandler = this;
        }
        
        private void OnDisable()
        {
            MainManager.UIHandler = null;
        }
    }
}
