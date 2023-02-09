using System.Collections;
using UnityEngine;
using Wonderland.Manager;

namespace Wonderland.GamePlay.InputManagement
{
    public class SwipeDetection : MonoBehaviour, IControls
    {
        public IEnumerator Detection()
        {
            yield break;
        }

        private void Awake()
        {
            Logging.InputLogger.Log(this + " Has Added In Scene");
        }
        
        private void OnDestroy()
        {
            Logging.InputLogger.Log(this + " Has Destroyed From Scene");
        }
    }
}
