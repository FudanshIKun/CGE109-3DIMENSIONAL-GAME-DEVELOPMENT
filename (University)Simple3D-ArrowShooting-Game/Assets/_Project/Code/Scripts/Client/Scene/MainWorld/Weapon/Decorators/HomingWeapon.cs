using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Wonderland.Client.MainWorld
{
    [Serializable]
    public class HomingWeapon : IWeaponDecorator
    {
        [Header("Status")]
        [ReadOnly] public Target lockedTarget;
        [ReadOnly] public float chargeAmount;
        [Header("Setting")]
        [SerializeField] private float chargeDuration = 1f;
        [SerializeField] private float middleChargePrecision = .1f;
        [SerializeField] private Ease chargeEase;
        [Header("Required Components")] 
        public PlayerWeapon weapon;
        [Header("UI")]
        public Slider precisionSlider;
        [Header("Particle")]
        [SerializeField] private ParticleSystem missingParticleSystem;
        [SerializeField] private ParticleSystem correctParticleSystem;
        
        [HideInInspector] public UnityEvent onReleaseCharge;
        [HideInInspector] public UnityEvent onRelease;
        [HideInInspector] public UnityEvent onTargetHit;
        private bool IsPerfectShot { get; set; }
        private bool WaitingForCooldown { get; set; }

        public void Releasing() // Either Make Abstract Base For IWeaponDecorator Or Make It Private So Weapon Cant Access it via Public Accessor :p
        {
            if (HalfCharge()) chargeAmount = .5f;
            if (FullCharge()) chargeAmount = 1;
            
            onRelease?.Invoke();

            if (chargeAmount >= 1 - middleChargePrecision || (chargeAmount >= .5f - middleChargePrecision && chargeAmount <= .5f + middleChargePrecision))
            {
                IsPerfectShot = chargeAmount >= .5f - middleChargePrecision && chargeAmount <= .5f + middleChargePrecision;
                if (correctParticleSystem.isPlaying) correctParticleSystem.Stop();
                ReleaseCollect();
            }
            else
            {
                if (missingParticleSystem.isPlaying) missingParticleSystem.Stop();
                ReleaseMissing();
            }
            
            weapon.StartCoroutine(weapon.ReleaseCoolDown());
        }

        public void TargetHit(Vector3 dir)
        {
            CustomLog.GamePlaySystem.Log("TargetHit: " + lockedTarget.gameObject.name);
            if (IsPerfectShot) weapon.StartCoroutine(weapon.SlowTime());
            onTargetHit?.Invoke();
            lockedTarget.Disable();
        }

        public void StartCharge()
        {
            if (AimSystem.currentTarget == null || !AimSystem.currentTarget.isAvailable) return;
            if (weapon.onCooldown)
            {
                if (!WaitingForCooldown) weapon.StartCoroutine(WaitForReleaseCooldownAndStartCharge());
            }
            else
            {
                if (weapon.isPerforming) return;
                DOVirtual.Float(0, 1, chargeDuration, SetChargeAmount).SetId(0).SetEase(chargeEase).OnComplete(() => weapon.isPerforming = false);
            }
        }
        
        public void StartCharge(Action onChargeCompleteCallback)
        {
            if (AimSystem.currentTarget == null || !AimSystem.currentTarget.isAvailable) return;
            DOVirtual.Float(0, 1, chargeDuration, SetChargeAmount).SetId(0).SetEase(chargeEase).OnComplete(onChargeCompleteCallback.Invoke);
        }

        public void Charging()
        {
            
        }

        public void ReleaseCharge()
        {
            if (AimSystem.currentTarget == null || !AimSystem.currentTarget.isAvailable) return;
            
            onReleaseCharge.Invoke();
            Releasing();
            AimSystem.storedTarget = null;
            SetChargeAmount(0);
            DOTween.Kill(0);
        }
        
        #region Methods

        private void ReleaseCollect()
        {
            CustomLog.GamePlaySystem.Log("ReleaseCollect");
            lockedTarget = AimSystem.currentTarget;
            var lockedTargetPos = lockedTarget.transform.position;
            correctParticleSystem.transform.position = lockedTargetPos;
            var shape = correctParticleSystem.shape;
            var dir = Quaternion.LookRotation(lockedTargetPos - GameplayHandler.Instance.player.transform.position).eulerAngles;
            shape.position = correctParticleSystem.transform.InverseTransformPoint(weapon.releasePoint.position);
            shape.rotation = new Vector3(0f, dir.y, 0f);
            correctParticleSystem.Play();
        }

        private void ReleaseMissing()
        {
            CustomLog.GamePlaySystem.Log("ReleaseMissing");
            lockedTarget = AimSystem.currentTarget;
            var lockedTargetPos = lockedTarget.transform.position;
            var shape = missingParticleSystem.shape;
            missingParticleSystem.transform.position = weapon.releasePoint.transform.position;
            missingParticleSystem.Play();
        }
        
        public void OnChangeTarget()
        {
            if (!weapon.isPerforming) return;
            precisionSlider.DOComplete();
            DOVirtual.Float(0, 1, chargeDuration, SetChargeAmount).SetId(0).SetEase(chargeEase);
        }

        public void OnLostTarget()
        {
            precisionSlider.DOComplete();
        }

        private void SetChargeAmount(float charge)
        {
            chargeAmount = charge;
            precisionSlider.value = chargeAmount;
        }
        
        private bool HalfCharge()
        {
            return chargeAmount >= .5f - middleChargePrecision && chargeAmount <= .5f + middleChargePrecision;
        }

        private bool FullCharge()
        {
            return chargeAmount >= 1 - middleChargePrecision;
        }
        
        private IEnumerator WaitForReleaseCooldownAndStartCharge()
        {
            WaitingForCooldown = true;
            yield return weapon.ReleaseCoolDown();
            yield return new WaitForSeconds(1);
            if (!weapon.isPerforming)
            {
                weapon.isPerforming = true;
                DOVirtual.Float(0, 1, chargeDuration, SetChargeAmount).SetId(0).SetEase(chargeEase).OnComplete(() => weapon.isPerforming = false);
            }
            
            WaitingForCooldown = false;
        }

        #endregion
    }
}
