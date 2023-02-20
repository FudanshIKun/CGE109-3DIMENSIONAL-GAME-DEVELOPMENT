using Unity.Android.Gradle;
using UnityEngine;
using Wonderland.Utility;
using Logging = Wonderland.Utility.Logging;

namespace Wonderland.GamePlay
{
    public class Cat : MonoBehaviour, ITouchable
    {
        public void TouchInteraction()
        {
            Logging.GamePlayLogger.Log(this + "has been touched");
        }
    }
}
