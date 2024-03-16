using System;
using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;
using Wonderland.Management;

namespace Wonderland.Client.MainWorld
{
    internal class MovementSystem : System
    {
        [Header("Status")]
        [ReadOnly] public bool isGrounded;
        [ReadOnly] public float currentAcceleration = 1;
        [Header("Settings")]
        [Header("Required Components")]
        [SerializeField] protected CinemachineFreeLook freeLookCam;

        #region Methods
        
        public void InputMagnitude()
        {
            var movement = new Vector2(InputHandler.Instance.Movement.x, InputHandler.Instance.Movement.y);
            var aiming = new Vector2(InputHandler.Instance.Aiming.x, InputHandler.Instance.Aiming.y);

            if (InputHandler.IsDragging)
            {
                freeLookCam.m_XAxis.Value += InputHandler.Instance.Dragging.x / GameplayHandler.Instance.setting.movement.turnDamping * 400 * Time.deltaTime;
                freeLookCam.m_YAxis.Value += -InputHandler.Instance.Dragging.y / GameplayHandler.Instance.setting.movement.turnDamping * Time.deltaTime;
            }

            if (InputHandler.IsMoving || InputHandler.IsAiming)
            {
                GameplayHandler.Instance.player.animator.SetBool(Player.IsAimingHash, InputHandler.IsAiming);
                
                if (movement.sqrMagnitude > 0.1f || InputHandler.IsAiming)
                {
                    GameplayHandler.Instance.player.animator.SetFloat(Player.MovementMagnitudeHash, movement.sqrMagnitude * currentAcceleration, .1f, Time.deltaTime);
                    Movement();
                    return;
                }
                
                GameplayHandler.Instance.player.animator.SetFloat(Player.MovementMagnitudeHash, 0, .1f, Time.deltaTime);
            }
            else
            {
                GameplayHandler.Instance.player.animator.SetFloat(Player.MovementMagnitudeHash, 0, .1f, Time.deltaTime);
                GameplayHandler.Instance.player.animator.SetBool(Player.IsAimingHash, false);
            }
        }

        private void Movement()
        {
            if (InputHandler.IsMoving && InputHandler.IsAiming)
            {
                Aim();
                Move(GameplayHandler.Instance.setting.movement.aimMovementSpeed);
                SetAimAnimation(new Vector3(InputHandler.Instance.Movement.x, 0, InputHandler.Instance.Movement.y).normalized);
            }
            else if (InputHandler.IsMoving)
            {
                Move(GameplayHandler.Instance.setting.movement.movementSpeed);
                var turning = Quaternion.Slerp(GameplayHandler.Instance.player.transform.rotation, Quaternion.LookRotation(GetMovementDir(), Vector3.up), GameplayHandler.Instance.setting.movement.rotationSpeed * currentAcceleration * Time.deltaTime);
                GameplayHandler.Instance.player.transform.rotation = turning;
            }
            else if (InputHandler.IsAiming)
            {
                Aim();
                SetAimAnimation(Vector3.zero);
            }
            
        }

        private void Move(float movementSpeed)
        {
            var controller = GameplayHandler.Instance.player.controller;
            var movement = GetMovementAmount() * (movementSpeed * (currentAcceleration * Time.deltaTime));
            controller.Move(movement);
        }

        private Vector3 GetMovementAmount()
        {
            var camDir = Quaternion.Euler(0, GameplayHandler.Instance.cam.transform.rotation.eulerAngles.y, 0);
            var moveAmount = new Vector3(InputHandler.Instance.Movement.x, 0, InputHandler.Instance.Movement.y);
            var movement = camDir * moveAmount;
            return movement;
        }
        
        private Vector3 GetMovementDir()
        {
            var camDir = Quaternion.Euler(0, GameplayHandler.Instance.cam.transform.rotation.eulerAngles.y, 0);
            var moveDir = new Vector3(InputHandler.Instance.Movement.x, 0, InputHandler.Instance.Movement.y).normalized;
            var movement = camDir * moveDir;
            return movement;
        }

        private void Aim()
        {
            freeLookCam.m_XAxis.Value += InputHandler.Instance.Aiming.x / GameplayHandler.Instance.setting.aiming.aimTurnDamping * 400 * Time.deltaTime;
            freeLookCam.m_YAxis.Value += -InputHandler.Instance.Aiming.y / GameplayHandler.Instance.setting.aiming.aimTurnDamping * Time.deltaTime;
            Quaternion turning;
            
            if (AimSystem.currentTarget != null)
            {
                CustomLog.GamePlaySystem.Log("Player Is Lock Rotation To CurrentTarget");
                var dirToTarget = (AimSystem.currentTarget.transform.position - GameplayHandler.Instance.player.transform.position).normalized;
                dirToTarget.y = 0;
                turning = Quaternion.Slerp(GameplayHandler.Instance.player.transform.rotation, Quaternion.LookRotation(dirToTarget, Vector3.up), GameplayHandler.Instance.setting.movement.rotationSpeed * Time.deltaTime);
                GameplayHandler.Instance.player.transform.rotation = turning;
            }
            else
            {
                var forward = GameplayHandler.Instance.cam.transform.forward;
                forward.y = 0;
                turning = Quaternion.Slerp(GameplayHandler.Instance.player.transform.rotation, Quaternion.LookRotation(forward, Vector3.up), GameplayHandler.Instance.setting.movement.rotationSpeed * Time.deltaTime);
                GameplayHandler.Instance.player.transform.rotation = turning;
            }
            
        }

        private void SetAimAnimation(Vector3 moveDir)
        {
            var localAimDir = GameplayHandler.Instance.player.transform.rotation * moveDir;
            GameplayHandler.Instance.player.animator.SetFloat(Player.XAimHash, localAimDir.x);
            GameplayHandler.Instance.player.animator.SetFloat(Player.YAimHash, localAimDir.z);
        }

        public void CheckGround()
        {
            var playerTransform = GameplayHandler.Instance.player.transform;
            isGrounded = Physics.Raycast(playerTransform.position + (playerTransform.rotation * Vector2.up * .05f), Vector3.down, .2f, GameplayHandler.Instance.setting.movement.groundLayerMask);
            GameplayHandler.Instance.player.animator.SetBool(Player.IsGroundedHash, isGrounded);
            GameplayHandler.Instance.player.animator.SetFloat(Player.GroundValueHash, isGrounded ? 0:1,.1f, Time.deltaTime);
        }

        #endregion
    }
}