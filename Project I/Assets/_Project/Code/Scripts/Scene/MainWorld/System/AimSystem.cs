using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Wonderland.Management;

namespace Wonderland.Scene.MainWorld
{
    internal class AimSystem : System
    {
        [Header("Status")]
        [Header("HomingWeapon Status")]
        [ReadOnly] public bool isTracking;
        [ReadOnly] public static Target currentTarget;
        [ReadOnly] public static Target storedTarget;
        
        [Header("Settings")]
        [Header("Required Components")]
        public PlayerWeapon weapon;
        [Header("HomingWeapon")] 
        [SerializeField] private float screenDistanceWeight;
        [SerializeField] private float positionDistanceWeight;
        [SerializeField] private RectTransform precisionRect;
        [SerializeField] private RectTransform focusPointRect;
        public UnityEvent onTargetChange = new ();
        public UnityEvent onTargetLost = new ();

        public void Aim()
        {
            if (weapon == null) return;
            switch (weapon.type)
            {
                case PlayerWeapon.Type.Melee:
                    return;
                case PlayerWeapon.Type.Ranged:
                    if (weapon.onCooldown) return;
                                
                    SetFocusPoint();
                    if (DetectionSystem.ReachableTargets.Count < 1 || !weapon.active)
                    {
                        currentTarget = null;
                        storedTarget = null;
                        precisionRect.gameObject.SetActive(false);
                        precisionRect.sizeDelta = Vector2.one;
                        onTargetLost?.Invoke();

                        return;
                    }
                                
                    CheckTargetChange();
                                
                    currentTarget = weapon.isPerforming? currentTarget : DetectionSystem.ReachableTargets[TargetIndex()];
                                
                    SetPrecisionIndicator();
                    isTracking = true;
                    break;
            }
        }
        
        private void SetFocusPoint()
        {
            Vector3 targetPosition;
            var position = focusPointRect.position;
            
            if (currentTarget == null)
            {
                targetPosition = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
                focusPointRect.position = targetPosition;
                return;
            }
            
            var screenPos = TargetToScreenPosition(currentTarget.body.position);
            targetPosition = new Vector3(screenPos.x, screenPos.y, 0);
            position = Vector3.MoveTowards(position, targetPosition, GameplayHandler.Instance.setting.aiming.focusPointDeltaSpeed * Time.deltaTime);
            focusPointRect.position = position;
        }

        private void CheckTargetChange()
        {
            if (storedTarget == currentTarget) return;
            storedTarget = currentTarget;
            precisionRect.DOComplete();
            precisionRect.DOScale(4, .2f).From();
            onTargetChange?.Invoke();
        }
        
        private int TargetIndex()
        {
            //Creates an array where the distances between the target and the screen/player will be stored
            var distances = new float[DetectionSystem.ReachableTargets.Count];

            //Populates the distances array with the sum of the Target distance from the screen center and the Target distance from the player
            for (var i = 0; i < DetectionSystem.ReachableTargets.Count; i++)
            {
                distances[i] =
                    (Vector2.Distance(GameplayHandler.Instance.cam.WorldToScreenPoint(DetectionSystem.ReachableTargets[i].transform.position), MiddleOfScreen()) * screenDistanceWeight)
                    +
                    (Vector3.Distance(GameplayHandler.Instance.player.transform.position, DetectionSystem.ReachableTargets[i].transform.position) * positionDistanceWeight);
            }

            //Finds the smallest of the distances
            var minDistance = Mathf.Min(distances);

            var index = 0;

            //Find the index number relative to the target with the smallest distance
            for (var i = 0; i < distances.Length; i++)
            {
                if (Math.Abs(minDistance - distances[i]) < 0.1f)
                    index = i;
            }

            return index;

        }

        private void SetPrecisionIndicator()
        {
            if (currentTarget == null) return;
            var playerPos = GameplayHandler.Instance.player.transform.position;
            var targetPos = currentTarget.body.position;
            var distanceFromTarget = Vector3.Distance(targetPos, playerPos);
            precisionRect.gameObject.SetActive(true);
            precisionRect.transform.position = TargetToScreenPosition(targetPos);
            precisionRect.sizeDelta = new Vector2(Mathf.Clamp(100 - (distanceFromTarget - GameplayHandler.Instance.setting.aiming.reactSizeMultiplier), 25, 80), Mathf.Clamp(100 - (distanceFromTarget- GameplayHandler.Instance.setting.aiming.reactSizeMultiplier), 25, 80));
        }

        public void DisablePrecisionIndicator()
        {
            currentTarget = null;
            storedTarget = null;
            precisionRect.gameObject.SetActive(false);
        }
        
        private Vector2 MiddleOfScreen()
        {
            return new Vector2(Screen.width / 2f, Screen.height / 2f);
        }
    }
}
