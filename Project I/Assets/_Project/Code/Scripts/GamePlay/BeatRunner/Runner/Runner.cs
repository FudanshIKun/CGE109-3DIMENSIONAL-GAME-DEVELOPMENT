using UnityEngine;
using Wonderland.Management;
using Wonderland.Serializables;

namespace Wonderland.GamePlay.BeatRunner.Running
{
    public class Runner : Player
    {
        #region Movement Settings

        [Header("Movement Settings")]
        public float moveSpeed = 5f;
        public float maxSpeed = 10f;
        public float turnSpeed = 5f;
        public Vector3 MoveVector { get; set; } 
        public int CurrentLane { get; set; }
        
        public float jumpHeight = 5;
        public float gravityScale = 5;
        public float fallGravityScale = 15;
        public bool IsGrounded { get; set; }
        public LayerMask groundLayer;

        #endregion

        public Runner(RunnerSetting setting) : base(
            FirebaseManager.GetCurrentUser().UserName, 
            FirebaseManager.GetCurrentUser().DisplayName)
        {
            
        }
        
        #region Fields
        
        [HideInInspector] public new Rigidbody rigidbody;
        [HideInInspector] public Animator animator;

        #endregion

        #region Behaviors

        private IRunnerBehavior CurrentBehavior { get; set; }

        private void ChangeBehavior(IRunnerBehavior behavior)
        {
            if (CurrentBehavior != null)
            {
                SwipeDetection.UpSwipe -= CurrentBehavior.UpSwipe;
                SwipeDetection.DownSwipe -= CurrentBehavior.DownSwipe;
                SwipeDetection.LeftSwipe -= CurrentBehavior.LefSwipe;
                SwipeDetection.RightSwipe -= CurrentBehavior.RightSwipe;
            }
            behavior.Runner = this;
            CurrentBehavior = behavior;
            SwipeDetection.UpSwipe += CurrentBehavior.UpSwipe;
            SwipeDetection.DownSwipe += CurrentBehavior.DownSwipe;
            SwipeDetection.LeftSwipe += CurrentBehavior.LefSwipe;
            SwipeDetection.RightSwipe += CurrentBehavior.RightSwipe;
        }

        #endregion

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            //ChangeBehavior(new RunningBehavior());
        }

        private void FixedUpdate()
        {
            if (CurrentBehavior != null)
            {
                CurrentBehavior.FixedUpdateBehavior();
            }
        }

        private void Update()
        {
            if (CurrentBehavior != null)
            {
                CurrentBehavior.UpdateBehavior();
            }
        }

        private void LateUpdate()
        {
            if (CurrentBehavior != null)
            {
                CurrentBehavior.LateUpdateBehavior();
            }
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