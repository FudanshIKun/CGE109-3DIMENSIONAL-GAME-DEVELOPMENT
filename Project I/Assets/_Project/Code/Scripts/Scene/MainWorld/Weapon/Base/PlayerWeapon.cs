using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Wonderland.Scene.MainWorld
{
    public abstract class PlayerWeapon : SerializedMonoBehaviour
    {
        [Header("Status")] 
        [ReadOnly] public bool active;
        [ReadOnly] public bool onCooldown;
        [ReadOnly] public bool isPerforming;
        [Header("Settings")] 
        [SerializeField] private float triggerCooldown;
        [SerializeField] protected float slowDownInterval;
        [Header("Weapon")]
        public Type type;
        public ParticleSystem hitParticle;
        [ShowIf("type", Type.Ranged), Header("Required Components")] public Transform releasePoint;

        private void Update()
        {
            if (InputHandler.IsAiming)
            {
                Charging();
            }
        }

        #region Abstract Methods
        
        public abstract void Enable();
        public abstract void Disable();
        public abstract void TargetHit(Vector3 dir);
        protected abstract void StartCharge();
        protected abstract void Charging();
        protected abstract void ReleaseCharge();
        
        public abstract void OnChangeTarget();
        public abstract void OnLostTarget();

        #endregion

        #region Methods

        public IEnumerator SlowTime()
        {
            const float scale = .2f;
            Time.timeScale = scale;
            yield return new WaitForSeconds(slowDownInterval / (1 / scale));
            Time.timeScale = 1;
        }

        public IEnumerator ReleaseCoolDown()
        {
            onCooldown = true;
            yield return new WaitForSeconds(triggerCooldown);
            onCooldown = false;
        }

        public IEnumerator DisableForPeriod()
        {
            active = false;
            yield return ReleaseCoolDown();
            active = true;
        }

        #endregion
        
        public enum Type
        {
            Melee,
            Ranged
        }
    }
}