using System;
using UnityEngine;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public abstract class WeaponSystem : MonoBehaviour
    {
        [Header("Status")]
        public bool active;
        public bool isPerforming;
        public Type type;
        public Style style;

        protected void OnEnable()
        {
            JoyStickDetection.OnAiming += StartCharge;
            JoyStickDetection.OnStopAim += CancelCharge;
        }

        protected void OnDisable()
        {
            JoyStickDetection.OnAiming -= StartCharge;
            JoyStickDetection.OnStopAim -= CancelCharge;
        }

        #region Methods

        protected virtual void CheckRelease()
        {
            
        }
        
        public virtual void StartCharge()
        {
            isPerforming = true;
        }

        public virtual void CancelCharge()
        {
            isPerforming = false;
        }

        #endregion

        public enum Type
        {
            Melee,
            Ranged
        }

        public enum Style
        {
            Directional,
            Tracking
        }
    }
}