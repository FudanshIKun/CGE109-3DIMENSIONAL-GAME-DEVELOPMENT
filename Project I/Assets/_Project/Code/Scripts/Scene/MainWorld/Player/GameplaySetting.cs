using System;
using UnityEngine;

namespace Wonderland.Scene.MainWorld
{
    [CreateAssetMenu(fileName = "Setting", menuName = "Setting/New GameplaySetting")]
    [Serializable]
    public class GameplaySetting : ScriptableObject
    {
        [Header("Settings")] 
        public LayerMaskSetting layermask;
        [Header("Player Settings")]
        public MovementSetting movement;
        public DetectionSetting detection;
        public AimingSetting aiming;
        public InteractionSetting interaction;
    }
    
    [Serializable]
    public struct MovementSetting
    {
        [Header("Movement Settings")]
        public float movementSpeed;
        public float rotationSpeed;
        public float turnDamping;
        public float aimMovementSpeed;

        [Header("LayerMask")] 
        public LayerMask groundLayerMask;
    }

    [Serializable]
    public struct DetectionSetting
    {
        [Header("Detection Settings")]
        public float viewRadius;
        public float viewAngle;
    }

    [Serializable]
    public struct AimingSetting
    {
        [Header("Aiming Settings")] 
        public float aimTurnDamping;

        [Header("UI")] 
        public float focusPointDeltaSpeed;
        public float reactSizeMultiplier;

        [Header("LayerMask")]
        public LayerMask targetLayerMask;
        public LayerMask obstacleLayerMask;
    }

    [Serializable]
    public struct InteractionSetting
    {
        [Header("Interaction Setting")]
        public float interactionRadius;
        [Header("UI")]
        
        [Header("Collision")] 
        public LayerMask interactableLayerMask;
    }
    
    [Serializable]
    public struct LayerMaskSetting
    {
        public LayerMask player;
        public LayerMask obstacle;
    }
}
