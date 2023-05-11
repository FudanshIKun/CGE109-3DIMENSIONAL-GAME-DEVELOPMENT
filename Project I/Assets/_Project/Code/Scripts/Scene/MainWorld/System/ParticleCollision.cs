using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Wonderland.Scene.MainWorld
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleCollision : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        private List<ParticleCollisionEvent> _collisionEvents;

        [Tooltip("Import Every SkinMeshRenderer Of Player In Here")]
        [SerializeField] private Renderer[] playerRenderer;
        [Tooltip("Player TrailParticleSystem")]
        [SerializeField] private ParticleSystem playerParticle;
        [Tooltip("AimSystem Of The Owner Of This ParticleObject")]
        [SerializeField] private bool isEnergy;
        private int _amount;

        private void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _collisionEvents = new List<ParticleCollisionEvent>();
        }

        private void Update()
        {
            if (isEnergy)
            {
                var particles = new ParticleSystem.Particle[_particleSystem.particleCount];
                _particleSystem.GetParticles(particles);

                for (var i = 0; i < particles.Length; i++)
                {
                    if (particles[i].remainingLifetime > 0f &&
                        Vector3.Distance(particles[i].position, transform.position) < 1f)
                    {
                        particles[i].remainingLifetime = 0f;
                        particles[i].startSize = 0f;

                        if (_amount < 1)
                        {
                            _amount++;
                            foreach (var part in playerRenderer)
                            {
                                part.material.DOFloat(1, "_Alpha", .2f).OnComplete(() => Complete(part));
                            }
                            
                            playerParticle.Play();
                        }
                    }
                }
                
                _particleSystem.SetParticles(particles, particles.Length);
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Target")) return;
            if (isEnergy) return;

            CustomLog.GamePlaySystem.Log("hit: " + other.gameObject.name);
            var collisionEventsNum = _particleSystem.GetCollisionEvents(other, _collisionEvents);
            GameplayHandler.Instance.player.currentWeapon.TargetHit(_collisionEvents[0].velocity);
            //GameplayHandler.Instance.DisablePrecisionSlider();
        }

        private void Complete(Renderer targetRenderer)
        {
            targetRenderer.material.DOFloat(0, "_Alpha", .3f).OnComplete(() => _amount = 0);
        }
    }
}
