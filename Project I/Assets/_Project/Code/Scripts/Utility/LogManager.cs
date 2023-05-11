using UnityEngine;

namespace Wonderland
{
    public static class CustomLog
    {
        // APIs
        public static readonly Logger Auth = new Logger(Debug.unityLogger.logHandler);
        public static readonly Logger Firestore = new Logger(Debug.unityLogger.logHandler);
        
        // Main System
        public static readonly Logger Manager = new(Debug.unityLogger.logHandler);
        public static readonly Logger Handler = new(Debug.unityLogger.logHandler);
        public static readonly Logger GameState = new(Debug.unityLogger.logHandler);
        
        // Input System
        public static readonly Logger InputSystem = new(Debug.unityLogger.logHandler);
        public static readonly Logger Detection = new(Debug.unityLogger.logHandler);
        
        // UserInterface
        public static readonly Logger UI = new(Debug.unityLogger.logHandler);
        
        // GamePlay System
        public static readonly Logger Object = new(Debug.unityLogger.logHandler);
        public static readonly Logger GamePlaySystem = new(Debug.unityLogger.logHandler);

        public static void LoadLogger()
        {
#if UNITY_EDITOR
            // APIs Logger
            Auth.logEnabled = true;
            Firestore.logEnabled = true;
            
            // System Logger
            Manager.logEnabled = false;
            Handler.logEnabled = true;
            GameState.logEnabled = true;
            
            // Input Logger
            InputSystem.logEnabled = false;
            Detection.logEnabled = true;
            
            // GamePlay Logger
            Object.logEnabled = true;
            GamePlaySystem.logEnabled = true;

            // UI Logger
            UI.logEnabled = true;
#endif
        }
    }
}