using UnityEngine;

namespace Wonderland
{
    public static class Logging
    {
        // APIs
        public static readonly Logger AuthLogger = new Logger(Debug.unityLogger.logHandler);
        public static readonly Logger FirestoreLogger = new Logger(Debug.unityLogger.logHandler);
        
        // Main System
        public static readonly Logger ManagerLogger = new(Debug.unityLogger.logHandler);
        public static readonly Logger HandlerLogger = new(Debug.unityLogger.logHandler);
        
        // Input System
        public static readonly Logger InputSystemLogger = new(Debug.unityLogger.logHandler);
        public static readonly Logger DetectionLogger = new(Debug.unityLogger.logHandler);
        
        // UserInterface
        public static readonly Logger UILogger = new(Debug.unityLogger.logHandler);
        
        // GamePlay System
        public static readonly Logger ObjectLogger = new(Debug.unityLogger.logHandler);
        public static readonly Logger GamePlaySystemLogger = new(Debug.unityLogger.logHandler);
        public static readonly Logger GamePlayStateLogger = new(Debug.unityLogger.logHandler);

        public static void LoadLogger()
        {
            // APIs
            AuthLogger.logEnabled = true;
            FirestoreLogger.logEnabled = true;
            
            // Main System
            ManagerLogger.logEnabled = false;
            HandlerLogger.logEnabled = true;
            
            // Input System
            InputSystemLogger.logEnabled = true;
            DetectionLogger.logEnabled = false;
            
            // GamePlay System
            ObjectLogger.logEnabled = true;
            GamePlaySystemLogger.logEnabled = true;
            GamePlayStateLogger.logEnabled = true;
            
            // UserInterface
            UILogger.logEnabled = true;
        }
    }
}