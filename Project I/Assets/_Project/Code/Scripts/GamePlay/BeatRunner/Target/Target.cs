using DG.Tweening;
using UnityEngine;

namespace Wonderland.GamePlay.BeatRunner
{
    public class Target : MonoBehaviour
    {
        [Header("Status")] 
        public bool isAvailable = true;
        public bool isReachable;
        [Header("Required")]
        [SerializeField] private Player player;
        [SerializeField] private Renderer visualRenderer;
        [Header("Particle")]
        [SerializeField] private ParticleSystem hitParticle;
        [SerializeField] private Collider detector;

        public void DisableTarget()
        {
            isAvailable = false;
            detector.enabled = false;
            visualRenderer.material.color = Color.black;
            hitParticle.Play();
        }
    }
}
