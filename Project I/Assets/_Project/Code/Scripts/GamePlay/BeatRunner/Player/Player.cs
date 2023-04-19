using System;
using UnityEngine;

namespace Wonderland.GamePlay.BeatRunner
{
    public class Player : MonoBehaviour
    {
        [Header("Status")] 
        public bool isGrounded;
        public bool isRunning;
        public bool isAiming;
        public bool isBoosting;
        [Header("Settings")]
        public PlayerSetting setting;
        [Header("Required Components")]
        [SerializeField] private MovementSystem movementSystem;
        [SerializeField] private AimSystem aimSystem;
        [SerializeField] private WeaponSystem weaponSystem;
        public CharacterController controller;
        public Animator animator;
        [Header("Hash String Animation Key")] 
        [SerializeField] private string boolGroundedHash;
        [SerializeField] private string floatGroundedHash;
        [SerializeField] private string boolAimingHash;
        [SerializeField] private string boolDashingHash;
        [SerializeField] private string boolBoostingHash;

        private IPlayerBehavior CurrentBehavior { get; set; }
        private EvaderBehavior  _evaderBehavior;
        
        public Player(PlayerSetting setting)
        {
            
        }
        
        private void Awake()
        {
            movementSystem = GetComponent<MovementSystem>();
            aimSystem = GetComponent<AimSystem>();
            weaponSystem = GetComponent<WeaponSystem>();
            animator = GetComponent<Animator>();
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

        #region Methods

        private void SetBehavior(Behavior behavior)
        {
            switch (behavior)
            {
                case Behavior.EvaderBehavior:
                    CurrentBehavior = _evaderBehavior;
                    CurrentBehavior.EnterBehavior();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(behavior), behavior, null);
            }
        }

        #endregion

        private enum Behavior {EvaderBehavior}

        #region Hashs

        public int IsGrounded => Animator.StringToHash(boolGroundedHash);
        public int GroundValue => Animator.StringToHash(floatGroundedHash);
        public int IsAiming => Animator.StringToHash(boolAimingHash);
        public int IsDashing => Animator.StringToHash(boolDashingHash);
        public int IsBoosting => Animator.StringToHash(boolBoostingHash);

        #endregion
    }
}
