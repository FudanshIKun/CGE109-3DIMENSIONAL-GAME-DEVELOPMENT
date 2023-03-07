using UnityEngine;

namespace Wonderland
{
    public static class Logging
    {
        public static Logger APILogger = new Logger(Debug.unityLogger.logHandler);
        public static Logger AuhLogger = new Logger(Debug.unityLogger.logHandler);
        public static Logger ManagerLogger = new Logger(Debug.unityLogger.logHandler);
        public static Logger InputSystemLogger = new Logger(Debug.unityLogger.logHandler);
        public static Logger InputControls = new Logger(Debug.unityLogger.logHandler);
        public static Logger UILogger = new Logger(Debug.unityLogger.logHandler);
        public static Logger GamePlayLogger = new Logger(Debug.unityLogger.logHandler);

        public static void LoadLogger()
        {
            // API Logger
            APILogger.logEnabled = true;
            AuhLogger.logEnabled = true;
            
            // Manager Logger
            ManagerLogger.logEnabled = true;
            
            // Input Logger
            InputSystemLogger.logEnabled = false;
            InputControls.logEnabled = false;
            
            // GamePlay Logger
            GamePlayLogger.logEnabled = true;
            
            // UserInterface Logger
            UILogger.logEnabled = true;
        }
    }
}
