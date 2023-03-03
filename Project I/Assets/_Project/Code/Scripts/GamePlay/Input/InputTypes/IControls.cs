using System;
using UnityEngine;
using Wonderland.Utility;

namespace Wonderland.GamePlay.Input
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
