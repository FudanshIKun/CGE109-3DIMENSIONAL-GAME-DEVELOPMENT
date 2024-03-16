using Sirenix.OdinInspector;
using UnityEngine;

namespace Wonderland.Client.MainWorld
{
    public abstract class System : SerializedMonoBehaviour
    {
        #region Methods

        /// <summary>
        /// Get World Position And Turn It To Screen Position And Clamp It Inside Screen
        /// </summary>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        protected Vector3 TargetToScreenPosition(Vector3 targetPosition)
        {
            if (GameplayHandler.Instance.cam == null) return Vector3.zero;
            var worldToScreenPos = GameplayHandler.Instance.cam.WorldToScreenPoint(targetPosition);
            var clampedPosition = new Vector3(Mathf.Clamp(worldToScreenPos.x, 0, Screen.safeArea.width), Mathf.Clamp(worldToScreenPos.y, 0, Screen.safeArea.height), worldToScreenPos.z);
            return clampedPosition; 
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="angleInDegrees"></param>
        /// <param name="angleIsGlobal"></param>
        /// <returns></returns>
        public static Vector3 DirFromAngle(Transform origin, float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal) {
                angleInDegrees += origin.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        #endregion
    }
}
