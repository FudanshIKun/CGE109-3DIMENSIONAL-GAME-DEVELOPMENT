using System;
using UnityEngine;

namespace Wonderland.Utility
{
    [CreateAssetMenu(fileName = "Setting", menuName = "Setting/New Scene Setting")]
    [Serializable]
    public class SceneSetting : ScriptableObject
    {
        public SceneSystem sceneSeting;
        public InputSystem inputSetting;
        public CameraSystem cameraSetting;
        

        [Serializable]
        public struct SceneSystem
        {
            public bool isGameScene;
            
            public enum WorldType
            {
                TwoDimensional,
                ThreeDimensional
            }

            public WorldType worldType;
        }
        
        [Serializable]
        public struct InputSystem
        {
            [Header("Handheld Inputs"),Tooltip("Currently Unity New Input System Doesn't Support Multi Finger Touch")]
            public bool isTouchable;
            public bool isSwipable;
        }
        
        [Serializable]
        public struct CameraSystem
        {
            public bool setCameraToStartPosition;
            public Vector3 cameraStartPosition;
            
            public enum CameraType
            {
                Perspective,
                Orthographic
            }

            public CameraType type;
        }
        
        
    }
}
