using System;
using UnityEngine;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public abstract class WeaponSystem : MonoBehaviour
    {
        [Header("Status")]
        public bool active;
        public bool onCooldown;
        public bool isPerforming;
        public Type type;
        public Style style;

        protected virtual void OnEnable()
        {
            JoyStickDetection.Controller.RightStick.OnStart += StartCharge;
            JoyStickDetection.Controller.RightStick.OnPerform += Charging;
            JoyStickDetection.Controller.RightStick.OnStop += CancelCharge;
        }

        protected virtual void OnDisable()
        {
            JoyStickDetection.Controller.RightStick.OnStart -= StartCharge;
            JoyStickDetection.Controller.RightStick.OnPerform -= Charging;
            JoyStickDetection.Controller.RightStick.OnStop -= CancelCharge;
        }

        #region Methods

        protected abstract void CheckRelease();
        protected abstract void StartCharge();
        protected abstract void Charging();
        public abstract void CancelCharge();
        public abstract void TargetHit(Vector3 dir);

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