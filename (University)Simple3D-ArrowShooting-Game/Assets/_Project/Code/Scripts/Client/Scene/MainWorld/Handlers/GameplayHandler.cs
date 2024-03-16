using Sirenix.OdinInspector;
using UnityEngine;

namespace Wonderland.Client.MainWorld
{
    public class GameplayHandler : SerializedMonoBehaviour
    {
        public static GameplayHandler Instance { get; private set; }
        [Header("Important Components")]
        public Player player;
        public Camera cam;
        public GameplaySetting setting;
        [Header("System")]
        [SerializeField] private TutorialSystem tutorialSystem;
        [SerializeField] private MovementSystem movementSystem;
        [SerializeField] private DetectionSystem detectionSystem;
        [SerializeField] private InteractionSystem interactionSystem;
        [SerializeField] private AimSystem aimSystem;
        [SerializeField] private EnergySystem energySystem;
        [SerializeField] private DialogueSystem dialogueSystem;
        [SerializeField] private TargetSystem targetSystem;
        [SerializeField] private CameraSystem cameraSystem;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            cam = Camera.main;
        }

        public bool SetUpGameplay()
        {
            CustomLog.Handler.Log("SetUpGameplay");
            InputHandler.Enable();
            tutorialSystem.StartTutorial();

            return true;
        }

        #region Methods

        public void Movement()
        {
            movementSystem.CheckGround();
            movementSystem.InputMagnitude();
        }

        public void Detect()
        {
            detectionSystem.Detection();
            interactionSystem.GenerateInteractionUI();
        }

        public void Aim()
        {
            aimSystem.Aim();
        }

        public void DisablePrecisionSlider()
        {
            aimSystem.DisablePrecisionIndicator();
        }

        public void EnableWeapon(PlayerWeapon weapon)
        {
            aimSystem.weapon = weapon;
            weapon.Enable();
            aimSystem.onTargetChange.AddListener(weapon.OnChangeTarget);
            aimSystem.onTargetLost.AddListener(weapon.OnLostTarget);
        }

        public void DisableWeapon(PlayerWeapon weapon)
        {
            aimSystem.weapon = null;
            weapon.Disable();
            aimSystem.onTargetChange.RemoveListener(weapon.OnChangeTarget);
            aimSystem.onTargetLost.RemoveListener(weapon.OnLostTarget);
        }

        public void IncreaseEnergy(float amount)
        {
            energySystem.IncreaseEnergyLevel(amount);
        }

        public void DecreaseEnergy(float amount)
        {
            energySystem.DecreaseEnergyLevel(amount);
        }

        public void StartDialogue() => dialogueSystem.StartDialogue();
        public void NextDialogue() => dialogueSystem.NextDialogue();

        #endregion
    }
}