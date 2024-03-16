using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Wonderland.Types;

namespace Wonderland.Client.MainWorld
{
    public abstract class Target : Objects
    {
        [Header("Status")] 
        public bool isAvailable = true;
        public bool isVisible;
        [Header("Required")]
        [SerializeField] protected Renderer visualRenderer;
        public Transform body;
        [Header("Particle")]
        [SerializeField] protected ParticleSystem diedParticle;
        [SerializeField] private Collider detector;

        private void OnBecameVisible()
        {
            isVisible = true;
        }

        private void OnBecameInvisible()
        {
            isVisible = false;
        }

        public virtual void Disable()
        {
            isAvailable = false;
            detector.enabled = false;
        }
    }
}
