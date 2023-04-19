using System;
using UnityEngine;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public class MovementSystem : MonoBehaviour
    {
        [Header("Status")]
        public float currentAcceleration = 1;
        [Header("Settings")]
        [Header("Required Components")] 
        [SerializeField] private Player player;

        private void Start()
        {
            player = GetComponent<Player>();
        }

        #region Methods

        public void MoveAndTurn()
        {
            var playerTransform = player.transform;
            if (JoyStickDetection.IsMoving)
            {
                var movement = new Vector3(JoyStickDetection.MovementAmount.x, 0,
                    JoyStickDetection.MovementAmount.y);
                player.controller.Move(movement * (player.setting.movement.movementSpeed * currentAcceleration * Time.deltaTime));
                player.isRunning = true;

                if (JoyStickDetection.IsAiming)
                {
                    var aimDir = Vector3.right * JoyStickDetection.AimAmount.x +
                                 Vector3.forward * JoyStickDetection.AimAmount.y;
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(aimDir,Vector3.up), player.setting.movement.rotationSpeed * Time.deltaTime);
                    player.isAiming = true;
                    return;
                }

                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(movement), player.setting.movement.movementSpeed * Time.deltaTime);
            }

            if (JoyStickDetection.IsAiming)
            {
                var aimDir = Vector3.right * JoyStickDetection.AimAmount.x +
                             Vector3.forward * JoyStickDetection.AimAmount.y;
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(aimDir,Vector3.up), player.setting.movement.rotationSpeed * Time.deltaTime);
                player.isAiming = true;
                return;
            }

            player.isRunning = false;
            player.isAiming = false;
        }
        
        private void CheckGround(Transform playerTransform)
        {
            player.isGrounded = Physics.Raycast(playerTransform.position + (transform.up * .05f), Vector3.down, .2f,
                player.setting.movement.groundLayerMask);
            player.animator.SetBool(player.IsGrounded, player.isGrounded);
            player.animator.SetFloat(player.GroundValue, player.isGrounded ? 0:1,.1f, Time.deltaTime);
        }

        #endregion
    }
}