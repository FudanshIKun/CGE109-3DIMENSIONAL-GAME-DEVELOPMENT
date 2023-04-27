using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public class AimSystem : MonoBehaviour
    {
        [Header("Status")] 
        public bool isAiming;
        public bool isTracking;
        public List<Target> reachableTargets;
        public Target currentTarget;
        public Target storedTarget;
        [Header("Required Components")]
        [SerializeField] private Camera cam;
        [SerializeField] private Player player;
        public WeaponSystem weaponSystem;
        [Header("UI")]
        [SerializeField] private RectTransform aimIndicator;
        [SerializeField] private RectTransform precisionRect;
        [SerializeField] private RectTransform focusPointRect;
        [HideInInspector] public Vector3 aimDirection;
        [HideInInspector] public UnityEvent onTargetChange;
        [HideInInspector] public UnityEvent onTargetLost;
        private PlayerSetting _setting;

        private void OnEnable()
        {
            cam = Camera.main;
            player = GetComponent<Player>();
            _setting = player.setting;
        }

        public void Aim()
        {
            if (weaponSystem.type == WeaponSystem.Type.Ranged)
            {
                if ( !weaponSystem.active) return;
                if (JoyStickDetection.IsAiming)
                {
                    var playerPos = player.transform.position;
                    var aimDir = new Vector3(JoyStickDetection.AimAmount.x, 0, JoyStickDetection.AimAmount.y);
                    aimIndicator.position = new Vector3(playerPos.x, 0.1f, playerPos.z);
                    aimIndicator.rotation = Quaternion.LookRotation(Vector3.down, aimDir);
                    aimIndicator.gameObject.SetActive(true);
                    aimDirection = aimDir;
                    
                    
                    if (weaponSystem.style != WeaponSystem.Style.Tracking) return;
                    if (Physics.Raycast(transform.position, aimDirection, out var hit, _setting.aiming.aimMaxDistance, _setting.aiming.targetLayer))
                    {
                        var target = hit.collider.GetComponent<Target>();
                        if (target == null) return;
                        if (!target.isAvailable || !target.isReachable) return;
                        
                        currentTarget = target;
                    }

                    if (currentTarget == null) return;
                    if (weaponSystem.onCooldown) return;
                    var targetDir = (currentTarget.transform.position - playerPos).normalized;
                    if (Vector3.Dot(targetDir, transform.forward) < 0)
                    {
                        currentTarget = null;
                        ClearStoredTarget();
                        precisionRect.gameObject.SetActive(false);
                        precisionRect.sizeDelta = Vector2.one;
                        onTargetLost?.Invoke();
                        return;
                    }
                    
                    SetFocusPoint();
                    CheckTargetChange();
                    SetPrecisionIndicator();
                }
                else
                {
                    ResetIndicators();
                    onTargetLost.Invoke();
                }
            }
        }

        #region Methods

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
            
            var screenPos = TargetToScreenPosition(currentTarget.transform.position);
            targetPosition = new Vector3(screenPos.x, screenPos.y, 0);
            position = Vector3.MoveTowards(position, targetPosition, _setting.aiming.focusPointDeltaSpeed * Time.deltaTime);
            focusPointRect.position = position;
        }

        /// <summary>
        /// Get World Position And Turn It To Screen Position And Clamp It Inside Screen
        /// </summary>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        private Vector3 TargetToScreenPosition(Vector3 targetPosition)
        {
            if (cam == null) return Vector3.zero;
            var worldToScreenPos = cam.WorldToScreenPoint(targetPosition);
            var clampedPosition = new Vector3(Mathf.Clamp(worldToScreenPos.x, 0, Screen.safeArea.width), Mathf.Clamp(worldToScreenPos.y, 0, Screen.safeArea.height), worldToScreenPos.z);
            return clampedPosition; 
        }

        private void CheckTargetChange()
        {
            if (storedTarget == currentTarget) return;
            storedTarget = currentTarget;
            precisionRect.DOComplete();
            precisionRect.DOScale(4, 2f).From();
            onTargetChange?.Invoke();
        }

        private void SetPrecisionIndicator()
        {
            if (currentTarget == null) return;
            var playerPos = player.transform.position;
            var targetPos = currentTarget.transform.position;
            var distanceFromTarget = Vector3.Distance(targetPos, playerPos);
            precisionRect.gameObject.SetActive(true);
            precisionRect.transform.position = TargetToScreenPosition(targetPos);
            precisionRect.sizeDelta = new Vector2(Mathf.Clamp(100 - (distanceFromTarget - _setting.aiming.reactSizeMultiplier), 25, 80), Mathf.Clamp(100 - (distanceFromTarget - _setting.aiming.reactSizeMultiplier), 25, 80));
        }

        private void ResetIndicators()
        {
            focusPointRect.gameObject.SetActive(false);
            aimIndicator.gameObject.SetActive(false);
            precisionRect.gameObject.SetActive(false);
            precisionRect.sizeDelta = Vector2.one;
        }

        public void ClearStoredTarget()
        {
            storedTarget = null;
        }

        public void StopFocus()
        {
            weaponSystem.CancelCharge();
            currentTarget = null;
        }

        #endregion
    }
}
