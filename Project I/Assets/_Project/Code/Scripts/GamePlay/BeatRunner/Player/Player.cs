using System;
using UnityEngine;

namespace Wonderland.GamePlay.BeatRunner
{
    public class Player : MonoBehaviour
    {
        [Header("Status")] 
        public bool isRunning;
        public bool isAiming;
        public bool isGrounded;
        public bool isBoosting;
        public PlayerMode mode;
        public float currentAcceleration;
        [Header("Settings")]
        public PlayerSetting setting;
        [Header("Required Components")]
        [SerializeField] private MovementSystem movementSystem;
        [SerializeField] private AimSystem aimSystem;
        [SerializeField] private WeaponSystem weaponSystem;
        public CharacterController controller;
        public Animator animator;

        private IPlayerBehavior CurrentBehavior { get; set; }
        private EvaderBehavior  _evaderBehavior;
        
        public Player(PlayerSetting setting)
        {
            
        }
        
        private void Awake()
        {
            movementSystem = GetComponent<MovementSystem>();
            aimSystem = GetComponent<AimSystem>();
            weaponSystem = GetComponentInChildren<WeaponSystem>();
            animator = GetComponentInChildren<Animator>();
            controller = GetComponent<CharacterController>();
            
        }
        
        private void OnEnable()
        {
            _evaderBehavior = new EvaderBehavior(movementSystem, aimSystem, weaponSystem);
        }

        private void Start()
        {
            SetBehavior(Behavior.EvaderBehavior);
        }

        private void Update()
        {
            CurrentBehavior?.UpdateBehavior();
        }

        private void LateUpdate()
        {
            CurrentBehavior?.LateUpdateBehavior();
        }

        #region Methods

        private void SetBehavior(Behavior behavior)
        {
            switch (behavior)
            {
                case Behavior.EvaderBehavior:
                    CurrentBehavior = _evaderBehavior;
                    CurrentBehavior?.EnterBehavior();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(behavior), behavior, null);
            }
        }

        #endregion

        private enum Behavior {EvaderBehavior}

        #region Hashs

        public static int IsGroundedHash => Animator.StringToHash("IsGrounded");
        public static int GroundValueHash => Animator.StringToHash("GroundValue");
        public static int IsAimingHash => Animator.StringToHash("IsAiming");
        public static int IsDashingHash => Animator.StringToHash("IsDashing");
        public static int IsBoostingHash => Animator.StringToHash("IsBoosting");

        #endregion
        
        public enum PlayerMode {Normal, Combat}
    }
}
