using System;
using DG.Tweening;
using UnityEngine;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public class MovementSystem : MonoBehaviour
    {
        [Header("Required Components")]
        [SerializeField] private Player player;

        #region Methods
        
        public void InputMagnitude()
        {
            var movementMagnitude = new Vector2(JoyStickDetection.MovementAmount.x, JoyStickDetection.MovementAmount.y).sqrMagnitude;
            var turningMagnitude = new Vector2(JoyStickDetection.AimAmount.x, JoyStickDetection.AimAmount.y).sqrMagnitude;

            if (movementMagnitude > 0.1f || turningMagnitude > 0.1f || player.isRunning)
            {
                player.animator.SetFloat(Player.MovementMagnitudeHash, (player.isRunning? 1 : movementMagnitude) * player.currentAcceleration, .1f, Time.deltaTime);
                MoveAndTurn();
            }
            else
            {
                player.animator.SetFloat(Player.MovementMagnitudeHash, 0, .1f, Time.deltaTime);
                
            }
        }

        private void MoveAndTurn()
        {
            var setting = player.setting.movement;
            var controller = player.controller;
            
            var moveDir = new Vector3(JoyStickDetection.MovementAmount.x, 0, JoyStickDetection.MovementAmount.y);
            var lookDir = new Vector3(JoyStickDetection.AimAmount.x, 0, JoyStickDetection.AimAmount.y);
            Quaternion turning;
            
            if (JoyStickDetection.IsMoving)
            {
                var movement = moveDir * (setting.movementSpeed * player.currentAcceleration * Time.deltaTime); ;
                controller.Move(movement);

                if (JoyStickDetection.IsAiming)
                {
                    Logging.GamePlaySystemLogger.Log("IsAiming In MovementSystem");
                    turning = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(lookDir, Vector3.up), setting.rotationSpeed * Time.deltaTime);
                    player.transform.rotation = turning;
                    player.isAiming = true;
                    return;
                }

                turning = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(movement), setting.movementSpeed * Time.deltaTime);
                player.transform.rotation = turning;
                return;
            }

            if (JoyStickDetection.IsAiming)
            {
                Logging.GamePlaySystemLogger.Log("IsAiming In MovementSystem");
                turning = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(lookDir, Vector3.up), setting.rotationSpeed * Time.deltaTime);
                player.transform.rotation = turning;
                player.isAiming = true;
                return;
            }
            
            player.isAiming = false;
        }

        public void CheckGround()
        {
            var playerTransform = player.transform;
            player.isGrounded = Physics.Raycast(playerTransform.position + (playerTransform.rotation * Vector2.up * .05f), Vector3.down, .2f, player.setting.movement.groundLayerMask);
            player.animator.SetBool(Player.IsGroundedHash, player.isGrounded);
            player.animator.SetFloat(Player.GroundValueHash, player.isGrounded ? 0:1,.1f, Time.deltaTime);
        }

        #endregion
    }
}