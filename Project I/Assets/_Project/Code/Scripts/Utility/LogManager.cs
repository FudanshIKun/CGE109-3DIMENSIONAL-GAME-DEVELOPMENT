using UnityEngine;

namespace Wonderland
{
    public static class Logging
    {
        // APIs
        public static readonly Logger AuthLogger = new Logger(Debug.unityLogger.logHandler);
        public static readonly Logger FirestoreLogger = new Logger(Debug.unityLogger.logHandler);
        
        // Main System
        public static readonly Logger ManagerLogger = new Logger(Debug.unityLogger.logHandler);
        
        // Input System
        public static readonly Logger InputSystemLogger = new Logger(Debug.unityLogger.logHandler);
        public static readonly Logger DetectionLogger = new Logger(Debug.unityLogger.logHandler);
        
        // UserInterface
        public static readonly Logger UILogger = new Logger(Debug.unityLogger.logHandler);
        
        // GamePlay System
        public static readonly Logger GamePlayLogger = new Logger(Debug.unityLogger.logHandler);

        public static void LoadLogger()
        {
            // APIs
            AuthLogger.logEnabled = true;
            FirestoreLogger.logEnabled = true;
            
            // Main System
            ManagerLogger.logEnabled = false;
            
            // Input System
            InputSystemLogger.logEnabled = false;
            DetectionLogger.logEnabled = false;
            
            // GamePlay System
            GamePlayLogger.logEnabled = true;
            
            // UserInterface
            UILogger.logEnabled = true;
        }
    }
}
