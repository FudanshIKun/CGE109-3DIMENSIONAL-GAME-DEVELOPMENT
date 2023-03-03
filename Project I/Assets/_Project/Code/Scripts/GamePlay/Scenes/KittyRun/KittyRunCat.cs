using System;
using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;
using Wonderland.Utility;
using Wonderland.GamePlay.Input;

namespace Wonderland.GamePlay.KittyRun
{
    public class KittyRunCat : Player
    {
        #region KittyRunCat Setting
        
        [Header("Physics Movement Setting")]
        [Header("Velocity")]
        public float runVelocity;
        public float jumpVelocity;
        [Header("Distance")]
        public float turnDistance;

        #endregion
        
        #region KittyRunCat Fields
        
        [HideInInspector] public Rigidbody _rigidbody;
        [HideInInspector] public Animator _animator;

        #endregion

        #region State Fields

        public KittyBaseState currentState;
        private KittyIdleState IdleState = new KittyIdleState();
        private KittyRunningState RunningState = new KittyRunningState();
        private KittyFinishedState FinishedState = new KittyFinishedState();

        #endregion
        
        #region Methods

        private void TurnLeft()
        {
            if (currentState == RunningState)
            {
                StartCoroutine(TurnLeftLogic());
            }
        }

        private IEnumerator TurnLeftLogic()
        {
            yield return new WaitForFixedUpdate();
            _rigidbody.MovePosition( new Vector3(_rigidbody.position.x - turnDistance, _rigidbody.position.y,_rigidbody.position.z));
        }

        private void TurnRight()
        {
            if (currentState == RunningState)
            {
                StartCoroutine(TurnRightLogic());
            }
        }
        
        private IEnumerator TurnRightLogic()
        {
            yield return new WaitForFixedUpdate();
            _rigidbody.MovePosition( new Vector3(_rigidbody.position.x + turnDistance, _rigidbody.position.y,_rigidbody.position.z));
        }

        private void Jump()
        {
            StartCoroutine(JumpLogic());
        }

        private IEnumerator JumpLogic()
        {
            yield return new WaitForFixedUpdate();
            var velocity = _rigidbody.velocity;
            velocity = new Vector3(velocity.x, jumpVelocity, velocity.z);
            _rigidbody.velocity = velocity;
            Logging.GamePlayLogger.Log("Jump : " + _rigidbody.velocity);
        }

        #endregion

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            currentState = RunningState;
            
            currentState.EnterState(this);
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdateState(this);
            Logging.GamePlayLogger.Log("FixedUpdate : " + _rigidbody.velocity);
        }

        private void Update()
        {
            currentState.UpdateState(this);
            Logging.GamePlayLogger.Log("Update : " + _rigidbody.velocity);
        }

        private void LateUpdate()
        {
            Logging.GamePlayLogger.Log("LateUpdate : " + _rigidbody.velocity);
        }

        private void OnEnable()
        {
            if (InputManager.Instance.gameObject.GetComponent<SwipeDetection>() != null)
            {
                SwipeDetection.LeftSwipe += TurnLeft;
                SwipeDetection.RightSwipe += TurnRight;
                SwipeDetection.UpSwipe += Jump;
            }
        }

        private void OnDisable()
        {
            if (InputManager.Instance != null && InputManager.Instance.gameObject.GetComponent<SwipeDetection>() != null)
            {
                SwipeDetection.LeftSwipe -= TurnLeft;
                SwipeDetection.RightSwipe -= TurnRight;
                SwipeDetection.UpSwipe -= Jump;
            }
        }
    }
}
