using UnityEngine;

namespace Wonderland.Management
{
    public abstract class SceneHandler : MonoBehaviour
    {
        [Header("Scene Settings")]
        public SceneSetting setting;

        public abstract void SetUpScene();
        public abstract void LaunchGame();
    }
}
