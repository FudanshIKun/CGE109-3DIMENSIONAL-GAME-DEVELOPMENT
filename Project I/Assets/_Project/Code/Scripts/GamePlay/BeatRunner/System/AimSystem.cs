using System;
using System.Collections.Generic;
using DG.Tweening;
using Fusion;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public class AimSystem : MonoBehaviour
    {
        [SerializeField] private PlayerSetting setting;
        [Header("Status")] 
        public bool isAiming;
        public bool isTracking;
        public List<Target> targets;
        public List<Target> reachableTargets;
        public Target currentTarget;
        public Target storedTarget;
        [Header("Settings")]
        [SerializeField] private float aimMaxDistance;
        [SerializeField] private LayerMask targetLayer;
        [Header("Required Components")]
        [SerializeField] private Camera cam;
        [SerializeField] private Player player;
        [SerializeField] private WeaponSystem weaponSystem;
        [Header("UI")]
        [SerializeField] private RectTransform aimIndicator;
        [SerializeField] private RectTransform precisionRect;
        [SerializeField] private float reactSizeMultiplier = 2;
        [SerializeField] private RectTransform focusPointRect;
        [SerializeField] private float focusPointDeltaSpeed = 200;

        [HideInInspector] public Vector3 aimDirection;
        [HideInInspector] public UnityEvent<Transform> onTargetChange;

        private void OnEnable()
        {
            cam = Camera.main;
            player = GetComponent<Player>();
            setting = player.setting;
            weaponSystem = GetComponentInChildren<WeaponSystem>();
        }

        private void Start()
        {
            SetAimCanvas();
        }

        private void Update()
        {
            Debug.DrawRay(transform.position, aimDirection);
        }

        public void Aim()
        {
            if (weaponSystem.type == WeaponSystem.Type.Ranged)
            {
                if ( !weaponSystem.active) return;
                if (JoyStickDetection.IsAiming)
                {
                    var aimDir = Vector3.forward *JoyStickDetection.AimAmount.y + Vector3.right * JoyStickDetection.AimAmount.x;
                    aimIndicator.position = new Vector3(player.transform.position.x, 0.1f, player.transform.position.z);
                    aimIndicator.gameObject.SetActive(true);
                    aimIndicator.rotation = Quaternion.LookRotation(Vector3.down, aimDir);
                    aimDirection = aimDir;
                    
                    if (weaponSystem.style == WeaponSystem.Style.Tracking && Physics.Raycast(transform.position, aimDirection, out var hit, aimMaxDistance,
                            targetLayer))
                    {
                        var target = hit.collider.GetComponent<Target>();
                        if (!target.isReachable) return;
                        currentTarget = target;
                    }

                    if (SetFocusPoint())
                    {
                        SetTargetIndicator();
                    }
                    
                    CheckTargetChange();
                    SetTargetIndicator();
                }
                else
                {
                    focusPointRect.gameObject.SetActive(false);
                    aimIndicator.gameObject.SetActive(false);
                    precisionRect.gameObject.SetActive(false);
                }
            }
        }

        #region Methods

        private void SetAimCanvas()
        {
            aimIndicator.gameObject.SetActive(false);
            aimIndicator.position = new Vector3(player.transform.position.x, 0.1f, player.transform.position.z);
        }

        private bool SetFocusPoint()
        {
            Vector3 targetPosition;
            var position = focusPointRect.position;
            
            if (currentTarget == null)
            {
                targetPosition = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
                focusPointRect.position = targetPosition;
                return false;
            }
            
            var screenPos = TargetToScreenPosition(currentTarget.transform.position);
            targetPosition = new Vector3(screenPos.x, screenPos.y, 0);
            position = Vector3.MoveTowards(position, targetPosition, focusPointDeltaSpeed * Time.deltaTime); 
            focusPointRect.position = position;

            return position == targetPosition;
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
        }

        private void SetTargetIndicator()
        {
            var targetDir = (currentTarget.transform.position - transform.position).normalized;
            if (Vector3.Dot(targetDir, transform.forward) < 0)
            {
                precisionRect.gameObject.SetActive(false);
                return;
            }
            precisionRect.gameObject.SetActive(true);
            precisionRect.transform.position = TargetToScreenPosition(currentTarget.transform.position);
            var distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
            precisionRect.sizeDelta = new Vector2(Mathf.Clamp(115 - (distanceFromTarget - reactSizeMultiplier), 25, 80), Mathf.Clamp(115 - (distanceFromTarget - reactSizeMultiplier), 25, 80));
        }

        public void StopFocus()
        {
            weaponSystem.CancelCharge();
            currentTarget = null;
        }

        #endregion
    }
}
