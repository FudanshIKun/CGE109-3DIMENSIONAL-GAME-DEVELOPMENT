using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Wonderland.GamePlay.BeatRunner
{
    public class RangedWeaponSystem : WeaponSystem
    {
        [Header("Settings")]
        [SerializeField] private float triggerCooldown = .5f;
        [SerializeField] private float chargeDuration = .8f;
        [SerializeField] private Ease chargeEase;
        [Header("GameObject")]
        [SerializeField] private GameObject shot;
        [Header("UI")] 
        public Slider precisionSlider;
        [Header("Particle")] public float middleChargePrecision = .5f;
        [SerializeField] private ParticleSystem missingEmission;
        [SerializeField] private ParticleSystem correctEmission;
        [SerializeField] private Transform releasePoint;
        private bool _releaseCooldown;
        private float _chargeAmount;

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        #region Methods

        protected override void CheckRelease()
        {
            
        }

        public override void StartCharge()
        {
            base.StartCharge();
        }

        public override void CancelCharge()
        {
            base.CancelCharge();
        }
        
        private bool HalfCharge()
        {
            return _chargeAmount > .5f - middleChargePrecision && _chargeAmount < .5f + middleChargePrecision;
        }

        private bool FullCharge()
        {
            return _chargeAmount >= 1 - middleChargePrecision;
        }

        private void ReleaseCorrect()
        {
            
        }

        private void ReleaseMissing()
        {
            
        }

        #endregion
    }
}
