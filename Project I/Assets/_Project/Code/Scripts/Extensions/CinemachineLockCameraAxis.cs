using UnityEngine;
using Cinemachine;

namespace Wonderland
{
    /// <summary>
    /// An add-on module for Cinemachine Virtual Camera that locks the camera's Z co-ordinate
    /// </summary>
    [ExecuteInEditMode] [SaveDuringPlay] [AddComponentMenu("")] // Hide in menu
    public class CinemachineLockCameraAxis : CinemachineExtension
    {
        [Header("X Axis")]
        public bool lockXAxis;
        [Tooltip("Lock the camera's X position to this value")] public float xPositionOffset = 10;
        [Space(2)]
        [Header("Y Axis")]
        public bool lockYAxis;
        [Tooltip("Lock the camera's Y position to this value")] public float yPositionOffset = 10;
        [Space(2)]
        [Header("Z Axis")]
        public bool lockZAxis;
        [Tooltip("Lock the camera's z position to this value")] public float zPositionOffset = 10;
 
        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage != CinemachineCore.Stage.Body) return;

            var pos = state.RawPosition;
            
            if (lockXAxis)
            {
                pos.x = xPositionOffset;
                state.RawPosition = pos;
            }
            
            if (lockYAxis)
            {
                pos.y = yPositionOffset;
                state.RawPosition = pos;
            }

            if (!lockZAxis) return;
            pos.z = zPositionOffset;
            state.RawPosition = pos;
        }
    }
}
