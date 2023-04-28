using System;
using UnityEngine;

namespace Wonderland.GamePlay.BeatRunner
{
    [CreateAssetMenu(fileName = "Setting", menuName = "Setting/New PlayerSetting")]
    [Serializable]
    public class PlayerSetting : ScriptableObject
    {
        public MovementSetting movement;
        public AimingSetting aiming;
        public WeaponSetting weapon;
    }
    
    [Serializable]
    public struct MovementSetting
    {
        [Header("Movement Settings")]
        public float movementSpeed;
        public float rotationSpeed;

        [Header("Acceleration Settings")] 
        public float runAcceleration;
        public float boostMultiplier;
        public float boostDuration;
        public float accelerateLerp;
        public float decelerateLerp;
        public float runRotationSpeed;
        public float chargeStrafeSpeed;

        [Header("Dash Settings")] 
        public float dashDistance;

        [Header("Collision Settings")] 
        public LayerMask groundLayerMask;
    }

    [Serializable]
    public struct AimingSetting
    {
        [Header("Aiming Settings")] 
        public float interactMaxDistance;
        public float aimMaxDistance;

        [Header("UI settings")] 
        public float focusPointDeltaSpeed;
        public float reactSizeMultiplier;

        [Header("Collision Setting")] 
        public LayerMask interactableLayerMask;
        public LayerMask targetLayerMask;
    }

    [Serializable]
    public struct WeaponSetting
    {
        
    }
}
