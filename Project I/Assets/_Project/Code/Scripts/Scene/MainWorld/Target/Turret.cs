using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Serialization;
using Wonderland.GamePlay;

namespace Wonderland.Scene.MainWorld
{
    public class Turret : Target
    {
        [Header("Status")] 
        public bool active = true;
        [Header("Setting")] 
        public float shootRange;
        public float shootCoolDown;
        public float shootDelayTime;
        public float aimDuration;
        [ReadOnly] public float aimSpeed = 15f;
        [ReadOnly] [SerializeField] private State state = State.Aiming;
        [ReadOnly] public float timer;
        [Header("Required Components")] 
        [SerializeField] private Transform aimConstrainTransform;
        [FormerlySerializedAs("aimingRig")] [SerializeField] private Rig aimingLayer;
        [SerializeField] private Weapon turretWeapon;
        public Transform releasePoint;
        public Transform targetPoint;

        private void Update()
        {
            if (!active)
            {
                if (isVisible)
                {
                    CustomLog.GamePlaySystem.Log(name + " Is Not Active");
                }
                return;
            }
            
            var distanceToPlayer = Vector3.Distance(GameplayHandler.Instance.player.transform.position, transform.position);
            var distanceToPlayerBody = Vector3.Distance(GameplayHandler.Instance.player.body.transform.position, releasePoint.position);
            
            // Check If Player Is In Range
            if (PlayerIsInRange(distanceToPlayer))
            {
                aimingLayer.weight = 1;
                var playerPosition = GameplayHandler.Instance.player.body.position;
                var dirToPlayer = (playerPosition - releasePoint.position).normalized;

                switch (state)
                {
                    case State.Aiming:
                    // Check if the player is behind cover
                    if (!Physics.Raycast(releasePoint.position, dirToPlayer, distanceToPlayerBody, GameplayHandler.Instance.setting.layermask.obstacle))
                    {
                        aimConstrainTransform.position = Vector3.Slerp(aimConstrainTransform.position, playerPosition, aimSpeed * Time.deltaTime);
                        Ray ray = default;
                        ray.origin = releasePoint.position;
                        ray.direction = dirToPlayer;

                        if (Physics.Raycast(ray, out var hit))
                        {
                            targetPoint.position = hit.point;
                        }
                        else
                        {
                            targetPoint.position = ray.origin + ray.direction * 1000.0f;
                        }
                        
                        if (timer <= 0)
                        {
                            state = State.Charging;
                            timer = shootDelayTime;
                        }
                        else
                        {
                            timer -= Time.deltaTime;
                        }
                    }
                    else
                    {
                        timer = aimDuration;
                    }

                    break;
            
                case State.Charging:
                    if (timer <= 0)
                    {
                        turretWeapon.FireBullet();
                        state = State.Cooldown;
                        timer = shootCoolDown;
                    }
                    else
                    {
                        timer -= Time.deltaTime;
                    }
                    break;

                case State.Cooldown:
                    if (timer <= 0)
                    {
                        state = State.Aiming;
                        timer = aimDuration;
                    }
                    else
                    {
                        timer -= Time.deltaTime;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                aimingLayer.weight = 0;
                timer = aimDuration;
            }
            
            turretWeapon.UpdateBullets(Time.deltaTime);
        }

        private bool PlayerIsInRange(float distanceToPlayer)
        {
            return distanceToPlayer <= shootRange;
        }

        public override void DisableTarget()
        {
            base.DisableTarget();
            active = false;
        }

        private enum State
        {
            Aiming,
            Charging,
            Cooldown
        }
    }
}
