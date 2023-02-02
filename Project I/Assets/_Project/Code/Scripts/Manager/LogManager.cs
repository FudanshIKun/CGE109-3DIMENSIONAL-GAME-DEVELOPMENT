using UnityEngine;

namespace Wonderland.Manager
{
    public static class Logging
    {
        public static Logger FirebaseLogger = new Logger(Debug.unityLogger.logHandler);
        public static Logger InputLogger = new Logger(Debug.unityLogger.logHandler);
        public static Logger GamePlayLogger = new Logger(Debug.unityLogger.logHandler);

        public static void LoadLogger()
        {
            // Call This Function On Application Start
            FirebaseLogger.logEnabled = true;
            InputLogger.logEnabled = true;
            GamePlayLogger.logEnabled = true;
        }
    }
}
