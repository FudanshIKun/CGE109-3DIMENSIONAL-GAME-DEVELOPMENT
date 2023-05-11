using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Wonderland.GamePlay;
using Wonderland.Types;

namespace Wonderland.Scene.MainWorld
{
    
    [RequireComponent(typeof(CharacterController))]
    public class Player : Objects
    {
        [Header("Settings")]
        public PlayerMode mode;
        [Header("Required Components")]
        [ReadOnly] public PlayerWeapon currentWeapon;
        [SerializeField] private GameObject currentCharacter;
        public CharacterController controller;
        public Animator animator;
        public Transform head;
        public Transform body;

        private IPlayerBehavior CurrentBehavior { get; set; }
        private NormalBehavior  _normalBehavior;
        
        public Player()
        {
            /*if (GameplayHandler.Instance.player == null)
            {
                GameplayHandler.Instance.player = this;
            }
            else
            {
                Destroy(gameObject);
            }*/
        }
        
        private void Awake()
        {
            currentWeapon = currentCharacter.GetComponentInChildren<PlayerWeapon>();
            animator = GetComponentInChildren<Animator>();
            controller = GetComponent<CharacterController>();
        }
        
        private void OnEnable()
        {
            _normalBehavior = new NormalBehavior(GameplayHandler.Instance, currentWeapon);
        }

        private void Start()
        {
            SetBehavior(Behavior.Normal);
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

        private void SetWeapon(PlayerWeapon weapon)
        {
            
        }

        private void SetBehavior(Behavior behavior)
        {
            switch (behavior)
            {
                case Behavior.Normal:
                    CurrentBehavior = _normalBehavior;
                    CurrentBehavior?.EnterBehavior();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(behavior), behavior, null);
            }
        }

        #endregion

        private enum Behavior {Normal}

        #region Hashs

        public static int MovementMagnitudeHash => Animator.StringToHash("MovementMagnitude");
        public static int IsAimingHash => Animator.StringToHash("IsAiming");
        public static int XAimHash => Animator.StringToHash("HorizontalAiming");
        public static int YAimHash => Animator.StringToHash("VerticalAiming");
        public static int IsGroundedHash => Animator.StringToHash("IsGrounded");
        public static int GroundValueHash => Animator.StringToHash("GroundValue");
        public static int IsDashingHash => Animator.StringToHash("IsDashing");

        #endregion
        
        public enum PlayerMode {Normal, Combat}
    }
}
