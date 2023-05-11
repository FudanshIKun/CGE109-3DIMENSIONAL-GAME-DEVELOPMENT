using UnityEngine;
using UnityEngine.Animations.Rigging;
using DG.Tweening;
using UnityEngine.Events;

namespace Wonderland.Scene.MainWorld
{
    public class Bow : PlayerWeapon
    {
        [Header("Decorators")]
        [SerializeField] private HomingWeapon homingDecorator;
        
        [Header("Settings")]
        [SerializeField] private float transitionSpeed = .1f;
        [SerializeReference] private float stringPullAmount = 0.2f;
        [Header("Required Components")]
        [Header("Procedural System")]
        [SerializeField] private Transform bowTransform;
        [SerializeField] private Transform bowVirtualTransform;
        [SerializeField] private LineRenderer bowLineRenderer;
        [SerializeField] private Transform bowLineCenter;

        public override void Enable()
        {
            InputHandler.Controller.RightStick.OnPressed += StartCharge;
            InputHandler.Controller.RightStick.OnRelease += ReleaseCharge;
            active = true;
        }

        public override void Disable()
        {
            InputHandler.Controller.RightStick.OnPressed -= StartCharge;
            InputHandler.Controller.RightStick.OnRelease -= ReleaseCharge;
            active = false;
        }

        public override void TargetHit(Vector3 dir)
        {
            homingDecorator.TargetHit(dir);
            hitParticle.transform.position = homingDecorator.lockedTarget.body.position;
            hitParticle.Play();
            //StartCoroutine(DisableForPeriod());
        }

        protected override void StartCharge()
        {
            if (!active) return;

            CustomLog.GamePlaySystem.Log(gameObject + " StartCharge");
            homingDecorator.StartCharge();
            isPerforming = true;
        }

        protected override void Charging()
        {
            if (!active) return;
            if (onCooldown) return;
            
            CustomLog.GamePlaySystem.Log(gameObject + " Charging");
            homingDecorator.Charging();
        }
        
        protected override void ReleaseCharge()
        {
            if (onCooldown) return;
            
            CustomLog.GamePlaySystem.Log( gameObject + " ReleaseCharge");
            homingDecorator.ReleaseCharge();
            isPerforming = false;
        }

        public override void OnChangeTarget()
        {
            homingDecorator.OnChangeTarget();
        }
        
        public override void OnLostTarget()
        {
            homingDecorator.OnLostTarget();
        }
    }
}
