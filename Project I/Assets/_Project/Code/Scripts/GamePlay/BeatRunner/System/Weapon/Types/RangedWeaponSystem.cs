using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public class RangedWeaponSystem : WeaponSystem
    {
        [Header("Status")]
        public float chargeAmount;
        [Header("Settings")] 
        [SerializeField] private float triggerCooldown;
        [SerializeField] private float minimumCharge = .1f;
        [SerializeField] private float chargeDuration = .8f;
        [SerializeField] public float middleChargePrecision = .5f;
        [SerializeField] private Ease chargeEase;
        [SerializeField] private float slowDownInterval;
        [Header("Required Components")]
        [SerializeField] private AimSystem aimSystem;
        [Header("UI")]
        public Slider precisionSlider;
        [Header("Particle")]
        [SerializeField] private ParticleSystem missingParticleSystem;
        [SerializeField] private ParticleSystem correctParticleSystem;
        [SerializeField] private Transform releasePoint;
        [Header("Events")] 
        public UnityEvent onReleaseCharge;
        public UnityEvent<float> onReleaseShot;
        public UnityEvent onTargetHit;
        private bool _perfectShot;
        private Target _lockedTarget;

        private void Start()
        {
            aimSystem.onTargetChange?.AddListener(OnTargetChange);
            aimSystem.onTargetLost?.AddListener(OnTargetLost);
        }

        #region Methods

        protected override void StartCharge()
        {
            if (!active) return;
            if (onCooldown) return;
            if (style != Style.Tracking) return;
            if (aimSystem.currentTarget == null || !aimSystem.currentTarget.isAvailable) return;
            if (chargeAmount < minimumCharge) return;

            Logging.GamePlaySystemLogger.Log("StartCharge");
            DOVirtual.Float(0, 1, chargeDuration, SetChargeAmount).SetId(0).SetEase(chargeEase);
        }

        protected override void Charging()
        {
            if (!active) return;
            if (onCooldown) return;
            if (style != Style.Tracking) return;
            if (aimSystem.currentTarget == null || !aimSystem.currentTarget.isAvailable) return;
            
            Logging.GamePlaySystemLogger.Log("Charging");
            isPerforming = true;
        }

        public override void CancelCharge()
        {
            if (style != Style.Tracking) return;
            if (!isPerforming) return;
            if (onCooldown) return;
            if (aimSystem.currentTarget == null || !aimSystem.currentTarget.isAvailable) return;
            if (chargeAmount < minimumCharge) return;
            
            Logging.GamePlaySystemLogger.Log("CancelCharge");
            onReleaseCharge.Invoke();
            CheckRelease();
            aimSystem.ClearStoredTarget();
            SetChargeAmount(0);
            isPerforming = false;
            DOTween.Kill(0);

        }

        protected override void CheckRelease()
        {
            StartCoroutine(ReleaseCoolDown());

            if (HalfCharge()) chargeAmount = .5f;
            if (FullCharge()) chargeAmount = 1;
            
            onReleaseShot?.Invoke(chargeAmount);

            if (chargeAmount >= 1 - middleChargePrecision || (chargeAmount >= .5f - middleChargePrecision && chargeAmount <= .5f + middleChargePrecision))
            {
                _perfectShot = chargeAmount >= .5f - middleChargePrecision && chargeAmount <= .5f + middleChargePrecision;
                if (correctParticleSystem.isPlaying) correctParticleSystem.Stop();
                ReleaseCollect();
                
            }
            else
            {
                if (missingParticleSystem.isPlaying) missingParticleSystem.Stop();
                ReleaseMissing();
            }
        }

        private void ReleaseCollect()
        {
            Logging.GamePlaySystemLogger.Log("ReleaseCollect");
            _lockedTarget = aimSystem.currentTarget;
            var lockedTargetPos = _lockedTarget.transform.position;
            correctParticleSystem.transform.position = lockedTargetPos;
            var shape = correctParticleSystem.shape;
            var dir = Quaternion.LookRotation(lockedTargetPos - transform.position).eulerAngles;
            shape.position = correctParticleSystem.transform.InverseTransformPoint(releasePoint.position);
            shape.rotation = new Vector3(0f, dir.y, 0f);
            correctParticleSystem.Play();
        }

        private void ReleaseMissing()
        {
            Logging.GamePlaySystemLogger.Log("ReleaseMissing");
            _lockedTarget = aimSystem.currentTarget;
            var lockedTargetPos = aimSystem.currentTarget.transform.position;
            var shape = missingParticleSystem.shape;
            missingParticleSystem.transform.position = releasePoint.transform.position;
            missingParticleSystem.Play();
        }

        private IEnumerator SlowTime()
        {
            const float scale = .2f;
            Time.timeScale = scale;
            yield return new WaitForSeconds(slowDownInterval / (1 / scale));
            Time.timeScale = 1;
        }

        private IEnumerator ReleaseCoolDown()
        {
            onCooldown = true;
            yield return new WaitForSeconds(triggerCooldown);
            onCooldown = false;
        }

        public override void TargetHit(Vector3 dir)
        {
            Logging.GamePlaySystemLogger.Log("TargetHit");
            onTargetHit?.Invoke();
            if (_perfectShot) StartCoroutine(SlowTime());

            active = true;
            onCooldown = false;
            _lockedTarget.DisableTarget();
            aimSystem.currentTarget = null;
        }

        private bool HalfCharge()
        {
            return chargeAmount > .5f - middleChargePrecision && chargeAmount < .5f + middleChargePrecision;
        }

        private bool FullCharge()
        {
            return chargeAmount >= 1 - middleChargePrecision;
        }

        private void SetChargeAmount(float charge)
        {
            chargeAmount = charge;
            precisionSlider.value = chargeAmount;
        }

        private void OnTargetChange()
        {
            precisionSlider.DOComplete();
            DOVirtual.Float(0, 1, chargeDuration, SetChargeAmount).SetId(0).SetEase(chargeEase);
        }

        private void OnTargetLost()
        {
            precisionSlider.DOComplete();
        }

        #endregion
    }
}
