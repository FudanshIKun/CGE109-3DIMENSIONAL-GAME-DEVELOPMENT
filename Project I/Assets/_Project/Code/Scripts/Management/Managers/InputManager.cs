using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Wonderland.InputActions;


namespace Wonderland.Management
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : IManager
    {
        [HideInInspector] public PlayerInput playerInput;
        [HideInInspector] public Camera mainCamera;
        private static HandheldInputAction HandheldInputAction { get; set; }
        private List<Controls> ControlsList { get; set; }
        
        private void UpdateInstance()
        {
            MainManager.Instance.InputManager.playerInput = playerInput;
            MainManager.Instance.InputManager.mainCamera = mainCamera;
        }
        
        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            playerInput.camera = Camera.main;
            mainCamera = playerInput.camera;
            MainManager.OnDestroyMainManager += UpdateInstance;
        }

        private void Start()
        {
            GameManager.LoadNewScene += DisableInputDetections;
            HandheldInputAction.Enable();
        }

        private void OnDisable()
        {
            HandheldInputAction.Disable();
            MainManager.OnDestroyMainManager -= UpdateInstance;
        }

        #region Methods
        
        private void DisableInputDetections()
        {
            Logging.ManagerLogger.Log("DisableInputDetections"); 
            if (ControlsList == null)
            {
                ControlsList = new List<Controls>();
            }
            else
            {
                foreach (var control in ControlsList)
                {
                    control.enabled = false;
                }
            }
        }

        public void EnableInputDetections()
        {
            Logging.ManagerLogger.Log("EnableInputDetections"); 
            ControlsList ??= new List<Controls>();
            HandheldInputAction ??= new HandheldInputAction();

            if (MainManager.Instance.sceneHandler.setting == null) return;
            var setting = MainManager.Instance.sceneHandler.setting;

            if (setting.sceneSetting.SceneType == SceneSetting.Setting.Type.GameScene && setting.inputSetting.EnableJoyStick)
            {
                var joystickController = FindController(setting.inputSetting.joystickSetting.joystickPrefab);
                if (gameObject.GetComponent<JoyStickDetection>() != null)
                {
                    var joyStickDetection = gameObject.GetComponent<JoyStickDetection>();
                    JoyStickDetection.Controller = joystickController;
                    joyStickDetection.enabled = true;
                    ControlsList.Add(joyStickDetection);
                }
                else
                {
                    var joyStickDetection = gameObject.AddComponent<JoyStickDetection>();
                    JoyStickDetection.Controller = joystickController;
                    joyStickDetection.enabled = true;
                    ControlsList.Add(joyStickDetection);
                }
            }
        }

        private JoystickController FindController(GameObject controllerPrefab)
        {
            JoystickController joystickController;
            if (MainManager.Instance.UIManager.joystickController != null)
            {
                joystickController = MainManager.Instance.UIManager.joystickController;
                return joystickController;
            }
            
            joystickController = Instantiate(controllerPrefab, transform.position, Quaternion.identity, MainManager.Instance.UIManager.UICanvas.transform).GetComponent<JoystickController>();
            return joystickController;
        }

        #endregion
    }
}
