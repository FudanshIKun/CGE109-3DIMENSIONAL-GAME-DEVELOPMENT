using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Wonderland.Management
{
    public abstract class SceneHandler : MonoBehaviour
    {
        [Header("Settings")]
        public SceneSetting sceneSetting;

        public abstract Task SetUpScene();
        public abstract void LaunchingGame();

        protected virtual void OnEnable()
        {
            MainManager.Instance.SceneHandler = this;
            MainManager.setting = sceneSetting;
        }
        
        protected virtual void OnDisable()
        {
            MainManager.Instance.SceneHandler = null;
            MainManager.setting = null;
        }
    }
}
