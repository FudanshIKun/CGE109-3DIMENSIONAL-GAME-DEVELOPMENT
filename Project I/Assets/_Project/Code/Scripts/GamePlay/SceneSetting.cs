using System;
using UnityEngine;

namespace Wonderland.GamePlay
{
    [CreateAssetMenu(fileName = "Setting", menuName = "Setting/New Scene Setting")]
    [Serializable]
    public class SceneSetting : ScriptableObject
    {
        public InputSystem inputSetting;
        public CameraSystem cameraSetting;
        
        [Serializable]
        public struct InputSystem
        {
            [Header("Handheld Inputs")]
            public bool isTouchable;
            public bool isPinchable;
            public bool isSwipable;
        }
        
        public struct CameraSystem
        {
            public enum CameraType
            {
                Perspective,
                Orthographic
            }

            public CameraType type;
        }
    }
}
