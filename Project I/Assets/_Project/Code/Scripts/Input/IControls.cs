using System;
using UnityEngine;

namespace Wonderland
{
    public class IControls : MonoBehaviour
    {
        public virtual void OnEnable()
        {
            Logging.InputSystemLogger.Log(this + "has been enabled");
        }

        public virtual void OnDisable()
        {
            Logging.InputSystemLogger.Log(this + "has been disabled");
        }
    }
}
