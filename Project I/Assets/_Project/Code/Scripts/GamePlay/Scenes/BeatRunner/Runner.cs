using System.Collections;
using UnityEngine;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public class Runner : Player
    {
        #region KittyRunCat Setting
        
        [Header("Physics Movement Setting")]
        [Header("Velocity")]
        public float runVelocity;
        public float jumpVelocity;
        [Header("Distance")]
        public float turnDistance;

        #endregion
        
        #region Fields
        
        [HideInInspector] public new Rigidbody rigidbody;
        [HideInInspector] public Animator animator;

        #endregion

        #region Behavior Fields

        

        #endregion

        #region Movement Methods

        public void TurnLeft()
        {
            var position = rigidbody.position;
            rigidbody.MovePosition( new Vector3(position.x - turnDistance, position.y,position.z));
        }

        public void TurnRight()
        {
            var position = rigidbody.position;
            rigidbody.MovePosition( new Vector3(position.x + turnDistance, position.y,position.z));
        }

        public void Jump()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(velocity.x, jumpVelocity, velocity.z);
            rigidbody.velocity = velocity;
        }

        #endregion

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            
        }

        private void FixedUpdate()
        {
            
        }

        private void Update()
        {
            
        }
    }
}
