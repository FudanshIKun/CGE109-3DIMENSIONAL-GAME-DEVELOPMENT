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
            [SerializeField] bool touchable;
            [SerializeField] public bool swipable;
            
            public bool Touchable => touchable;
            public bool Swipable => swipable;
        }
    }
}
