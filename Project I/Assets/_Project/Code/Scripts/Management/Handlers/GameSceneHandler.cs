using System;
using UnityEngine;

namespace Wonderland.Management
{
    public class GamesceneHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            MainManager.GameSceneHandler = this;
        }

        private void OnDisable()
        {
            MainManager.GameSceneHandler = null;
        }
    }
}
