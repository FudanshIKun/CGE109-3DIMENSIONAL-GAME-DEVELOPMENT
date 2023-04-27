using System;
using UnityEngine;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public class MovementSystem : MonoBehaviour
    {
        [Header("Required Components")]
        [SerializeField] private Player player;

        #region Methods

        public void MoveAndTurn()
        {
            var setting = player.setting.movement;
            var controller = player.controller;
            
            var moveDir = new Vector3(JoyStickDetection.MovementAmount.x, 0, JoyStickDetection.MovementAmount.y);
            var lookDir = new Vector3(JoyStickDetection.AimAmount.x, 0, JoyStickDetection.AimAmount.y);
            Quaternion turning;
            
            if (JoyStickDetection.IsMoving)
            {
                var movement = moveDir * (setting.movementSpeed * player.currentAcceleration * Time.deltaTime);
                CheckNegativeEffects(movement);
                controller.Move(movement);
                player.isRunning = true;

                if (JoyStickDetection.IsAiming)
                {
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
                turning = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(lookDir, Vector3.up), setting.rotationSpeed * Time.deltaTime);
                player.transform.rotation = turning;
                player.isAiming = true;
                return;
            }

            player.isRunning = false;
            player.isAiming = false;
        }
        
        private void CheckGround(Transform playerTransform)
        {
            player.isGrounded = Physics.Raycast(playerTransform.position + (player.transform.rotation * Vector2.up * .05f), Vector3.down, .2f, player.setting.movement.groundLayerMask);
            player.animator.SetBool(Player.IsGroundedHash, player.isGrounded);
            player.animator.SetFloat(Player.GroundValueHash, player.isGrounded ? 0:1,.1f, Time.deltaTime);
            if (player.isGrounded)
            {
                
            }
        }

        private void CheckNegativeEffects(Vector3 movement)
        {
            
        }

        #endregion
        
        /*public struct MovementJob : IJob
        {

            public MovementJob()
            {

            }

            private void MoveAndTurn()
            {
                var setting = _player.setting.movement;
                var controller = _player.controller;
            
                var moveDir = new Vector3(JoyStickDetection.MovementAmount.x, 0, JoyStickDetection.MovementAmount.y);
                var lookDir = new Vector3(JoyStickDetection.AimAmount.x, 0, JoyStickDetection.AimAmount.y);
                Quaternion turning;
            
                if (JoyStickDetection.IsMoving)
                {
                    var movement = moveDir * (setting.movementSpeed * _player.currentAcceleration * Time.deltaTime);
                    controller.Move(movement);
                    _player.isRunning = true;

                    if (JoyStickDetection.IsAiming)
                    {
                        turning = Quaternion.Slerp(_player.transform.rotation, Quaternion.LookRotation(lookDir, Vector3.up), setting.rotationSpeed * Time.deltaTime);
                        _player.transform.rotation = turning;
                        _player.isAiming = true;
                        return;
                    }

                    turning = Quaternion.Slerp(_player.transform.rotation, Quaternion.LookRotation(movement), setting.movementSpeed * Time.deltaTime);
                    _player.transform.rotation = turning;
                    return;
                }

                if (JoyStickDetection.IsAiming)
                {
                    turning = Quaternion.Slerp(_player.transform.rotation, Quaternion.LookRotation(lookDir, Vector3.up), setting.rotationSpeed * Time.deltaTime);
                    _player.transform.rotation = turning;
                    _player.isAiming = true;
                    return;
                }

                _player.isRunning = false;
                _player.isAiming = false;
            }
            
            public void Execute()
            {
                MoveAndTurn();
            }
        }*/
    }
}