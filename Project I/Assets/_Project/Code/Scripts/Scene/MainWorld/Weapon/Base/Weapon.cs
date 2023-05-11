using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Wonderland.Scene.MainWorld
{
    public class Weapon : MonoBehaviour
    {
        [Header("Status")] 
        [ReadOnly] public bool isFiring;
        [ReadOnly] public readonly List<Bullet> bullets = new ();
        [Header("Setting")] 
        [Range(1f, 1000f)] public float bulletSpeed = 100f;
        public float bulletDrop;
        public float fireRate;
        public float maxLiftTime;
        public FireMode fireMode;
        [Header("Required Components")] 
        [SerializeField] private ParticleSystem muzzleEffect;
        [SerializeField] private TrailRenderer shotEffect;
        [SerializeField] private ParticleSystem hitEffect;
        [SerializeField] private Transform raycastOrigin;
        [SerializeField] private Transform raycastDestination;
        
        private Ray _ray;
        private RaycastHit _rayHit;
        private float _accumulatedTime;
        
        public void StartFire()
        {
            isFiring = true;
            _accumulatedTime = 0.0f;
            FireBullet();
        }

        public void Firing(float deltaTime)
        {
            if (fireMode != FireMode.Auto) return;
            _accumulatedTime += deltaTime;
            var fireInterval = 1.0f / fireRate;
            while (_accumulatedTime >=  0.0f)
            {
                FireBullet();
                _accumulatedTime -= fireInterval;
            }
        }

        public void StopFire()
        {
            isFiring = false;
        }

        public void UpdateBullets(float deltaTime)
        {
            SimulateBullets(deltaTime);
            DestroyBullets();
        }
        
        private void SimulateBullets(float deltaTime)
        {
            bullets.ForEach(bullet =>
            {
                var p0 = GetBulletPosition(bullet);
                bullet.lifeTime += deltaTime;
                var p1 = GetBulletPosition(bullet);
                RaycastSegment(p0, p1, bullet);
            });
        }
        
        private void DestroyBullets()
        {
            bullets.RemoveAll(bullet => bullet.lifeTime >= maxLiftTime);
        }

        public void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
        {
            var direction = end - start;
            var distance = direction.magnitude;
            _ray.origin = start;
            _ray.direction = direction;
            if (Physics.Raycast(_ray, out _rayHit, distance))
            {
                var hitCollider = _rayHit.collider;
                /*if (hitCollider.gameObject == transform.parent.gameObject)
                {
                    bullets.Remove(bullet);
                    return;
                }*/
                
                CustomLog.GamePlaySystem.Log(gameObject.transform.parent.name + " Hit " + hitCollider.name);
                
                var effectTransform = hitEffect.transform;
                effectTransform.position = _rayHit.point;
                effectTransform.forward = _rayHit.normal;
                hitEffect.Play();

                bullet.trailEffect.transform.position = _rayHit.point;
                bullet.lifeTime = maxLiftTime;
            }
            else
            {
                bullet.trailEffect.transform.position = end;
            }
        }

        public void FireBullet()
        {
            var origin = raycastOrigin.position;
            var velocity = (raycastDestination.position - origin).normalized * bulletSpeed;
            var bullet = CreateBullet(origin, velocity);
            if (muzzleEffect != null) muzzleEffect.Emit(1);
            bullets.Add(bullet);
        }

        private Bullet CreateBullet(Vector3 position, Vector3 velocity)
        {
            var bullet = new Bullet
            {
                initialPosition = position,
                initialVelocity = velocity,
                lifeTime = 0.0f
            };
            
            if (shotEffect == null) return bullet;
            bullet.trailEffect = Instantiate(shotEffect, position, Quaternion.identity);
            bullet.trailEffect.AddPosition(position);
            return bullet;
        }

        private Vector3 GetBulletPosition(Bullet bullet)
        {
            var gravity = Vector3.down * bulletDrop;
            return (bullet.initialPosition) + (bullet.initialVelocity * bullet.lifeTime) + gravity * (0.5f * bullet.lifeTime * bullet.lifeTime);
        }

        public enum FireMode
        {
            Semi,
            Auto
        }
    }
}