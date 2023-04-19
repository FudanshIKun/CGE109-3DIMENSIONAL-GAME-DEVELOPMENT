using System;
using UnityEngine;

namespace Wonderland
{
    [CreateAssetMenu(fileName = "Setting", menuName = "Setting/New Scene Setting")]
    [Serializable]
    public class SceneSetting : ScriptableObject
    {
        public Setting sceneSetting;
        public InputSetting inputSetting;

        [Serializable]
        public struct Setting
        {
            public enum Type
            {
                NormalScene,
                GameScene
            }

            [Header("Scene Settings")] 
            [SerializeField] Type sceneType;
            public Type SceneType => sceneType;
        }
        
        [Serializable]
        public struct InputSetting
        {
            [Header("Handheld Input Settings")]
            //[SerializeField] private bool swipable;
            [SerializeField] private bool enableJoyStick;
            public JoystickSetting joystickSetting;
            
            //public bool Swipable => swipable;
            public bool EnableJoyStick => enableJoyStick;
        }
        
        [Serializable]
        public struct JoystickSetting
        {
            public GameObject joystickPrefab;
        }
    }
}
