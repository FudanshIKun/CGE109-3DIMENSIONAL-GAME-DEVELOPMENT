using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Wonderland
{
    [CreateAssetMenu(fileName = "Setting", menuName = "Setting/New Scene Setting")]
    [Serializable]
    public class SceneSetting : ScriptableObject
    {
        public Scene sceneSetting;
        public Input inputSetting;
        public Camera cameraSetting;
        

        [Serializable]
        public struct Scene
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
        public struct Input
        {
            [Header("Handheld Inputs")]
            public bool touchable;
            public bool swipable;
        }
        
        [Serializable]
        public struct Camera
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
