using UnityEngine;
using Wonderland.API;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner.Runner
{
    public class Runner : Player
    {
        #region Setting

        [Header("Physics Settings")]

        #region Running Settings

        [SerializeField] float moveSpeed;
        [SerializeField] float maxSpeed;

        #endregion

        #region Jumping Settings

        [SerializeField] float jumpHeight = 5;
        [SerializeField] float gravityScale = 5;
        [SerializeField] float fallGravityScale = 15;
        public bool IsGrounded { get; private set; }
        public LayerMask groundLayer;

        #endregion
        
        #endregion
        
        public Runner() : base(
            AuthAPI.GetCurrentUser().UserName, 
            AuthAPI.GetCurrentUser().DisplayName)
        {
            
        }
        
        #region Fields
        
        [HideInInspector] public new Rigidbody rigidbody;
        [HideInInspector] public Animator animator;

        #endregion

        #region Behavior Fields

        private IRunningBehavior _runningBehavior;

        #endregion

        #region Methods

        private void Movement()
        {
            
        }

        private void Interaction()
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
            Movement();
        }

        private void Update()
        {
            Interaction();
        }

        private void LateUpdate()
        {
            
        }
    }
}
