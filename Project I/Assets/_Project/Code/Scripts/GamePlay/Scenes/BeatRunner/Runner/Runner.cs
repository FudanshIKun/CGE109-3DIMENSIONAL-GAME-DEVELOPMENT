using System;
using System.Linq.Expressions;
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
            FirebaseManager.GetCurrentUser().UserName, 
            FirebaseManager.GetCurrentUser().DisplayName)
        {
            
        }
        
        #region Fields
        
        [HideInInspector] public new Rigidbody rigidbody;
        [HideInInspector] public Animator animator;

        #endregion

        #region Behavior Fields

        private IRunnerBehavior CurrentBehavior { get; set; }

        #endregion

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            if (CurrentBehavior != null)
            {
                SwipeDetection.UpSwipe += CurrentBehavior.UpSwipe;
                SwipeDetection.DownSwipe += CurrentBehavior.DownSwipe;
                SwipeDetection.LeftSwipe += CurrentBehavior.LefSwipe;
                SwipeDetection.RightSwipe += CurrentBehavior.RightSwipe;
            }
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

        private void LateUpdate()
        {
            
        }

        private void OnDisable()
        {
            if (CurrentBehavior != null)
            {
                SwipeDetection.UpSwipe -= CurrentBehavior.UpSwipe;
                SwipeDetection.DownSwipe -= CurrentBehavior.DownSwipe;
                SwipeDetection.LeftSwipe -= CurrentBehavior.LefSwipe;
                SwipeDetection.RightSwipe -= CurrentBehavior.RightSwipe;
            }
        }
    }
}
