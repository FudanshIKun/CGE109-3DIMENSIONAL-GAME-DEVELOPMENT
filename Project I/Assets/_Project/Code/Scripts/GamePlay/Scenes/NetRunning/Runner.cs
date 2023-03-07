using System.Collections;
using UnityEngine;
using Wonderland;

namespace Wonderland.GamePlay.NetRunning
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
        
        #region KittyRunCat Fields
        
        [HideInInspector] public Rigidbody _rigidbody;
        [HideInInspector] public Animator _animator;

        #endregion

        #region State Fields

        public BaseState currentState;
        private IdleState Idle = new IdleState();
        private RunningState Running = new RunningState();
        private FinishedState Finished = new FinishedState();

        #endregion

        #region State Methods

        

        #endregion
        
        #region Movement Methods

        public void TurnLeft()
        {
            if (currentState == Running)
            {
                StartCoroutine(TurnLeftLogic());
            }
        }

        private IEnumerator TurnLeftLogic()
        {
            yield return new WaitForFixedUpdate();
            _rigidbody.MovePosition( new Vector3(_rigidbody.position.x - turnDistance, _rigidbody.position.y,_rigidbody.position.z));
        }

        public void TurnRight()
        {
            if (currentState == Running)
            {
                StartCoroutine(TurnRightLogic());
            }
        }
        
        private IEnumerator TurnRightLogic()
        {
            yield return new WaitForFixedUpdate();
            _rigidbody.MovePosition( new Vector3(_rigidbody.position.x + turnDistance, _rigidbody.position.y,_rigidbody.position.z));
        }

        public void Jump()
        {
            StartCoroutine(JumpLogic());
        }

        private IEnumerator JumpLogic()
        {
            yield return new WaitForFixedUpdate();
            var velocity = _rigidbody.velocity;
            velocity = new Vector3(velocity.x, jumpVelocity, velocity.z);
            _rigidbody.velocity = velocity;
        }

        #endregion

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            currentState = Running;
            
            currentState.EnterState(this);
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdateState(this);
        }

        private void Update()
        {
            currentState.UpdateState(this);
        }
    }
}
