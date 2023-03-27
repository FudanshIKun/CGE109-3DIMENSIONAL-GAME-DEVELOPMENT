using System;
using UnityEngine;

namespace Wonderland.Management
{
    public class Controls : MonoBehaviour
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
