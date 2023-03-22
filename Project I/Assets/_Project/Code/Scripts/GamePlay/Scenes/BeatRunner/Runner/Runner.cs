using System;
using System.Collections;
using UnityEngine;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner.Runner
{
    public class Runner : Player
    {
        #region Setting
        
        [Header("Physics Settings")]
        [Header("Running Behavior")]
        
        #endregion
        
        #region Fields
        
        [HideInInspector] public new Rigidbody rigidbody;
        [HideInInspector] public Animator animator;

        #endregion

        #region Behavior Fields

        private IRunningBehavior _runningBehavior;

        #endregion

        #region Behavior Methods

        private void Running()
        {
            
        }

        private void Jumping()
        {
            
        }

        private void TurnLeft()
        {
            
        }
        
        private void TurnRight()
        {
            
        }

        #endregion

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            
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
