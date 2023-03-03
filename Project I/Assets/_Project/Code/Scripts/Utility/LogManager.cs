using UnityEngine;

namespace Wonderland.Utility
{
    public static class Logging
    {
        public static Logger UILogger = new Logger(Debug.unityLogger.logHandler);
        public static Logger FirebaseLogger = new Logger(Debug.unityLogger.logHandler);
        public static Logger InputSystemLogger = new Logger(Debug.unityLogger.logHandler);
        public static Logger InputControls = new Logger(Debug.unityLogger.logHandler);
        public static Logger GamePlayLogger = new Logger(Debug.unityLogger.logHandler);

        public static void LoadLogger()
        {
            // Firebase Logger
            FirebaseLogger.logEnabled = true;
            
            // Input Logger
            InputSystemLogger.logEnabled = false;
            InputControls.logEnabled = false;
            
            // GamePlay Logger
            GamePlayLogger.logEnabled = true;
            
            // UserInterface Logger
            UILogger.logEnabled = false;
        }
    }
}
