using UnityEngine;
using Cinemachine;
using UnityEngine.Serialization;

namespace Wonderland
{
    /// <summary>
    /// An add-on module for Cinemachine Virtual Camera that locks the camera's Z co-ordinate
    /// </summary>
    [ExecuteInEditMode] [SaveDuringPlay] [AddComponentMenu("")] // Hide in menu
    public class LockCameraY : CinemachineExtension
    {
        [FormerlySerializedAs("m_YPosition")] [Tooltip("Lock the camera's Y position to this value")]
        public float mYPosition = 10;
 
        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                pos.y = mYPosition;
                state.RawPosition = pos;
            }
        }
    }
}
