using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Wonderland.InputActions;
using Wonderland.Manager;


namespace Wonderland.GamePlay.InputManagement
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour, IUserControls
    {
        #region Singleton

        public static InputManager Instance;

        private void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Instance.playerInput = playerInput;
                Destroy(gameObject);
            }
        }

        #endregion

        private PlayerInput playerInput;
        public MobileInputActions _MobileInput;
        List<IControls> _controlsList;

        #region Methods

        public void CompileControlsInScene()
        {
            Logging.InputLogger.Log("CompileControls");
            if (_controlsList == null)
            {
                _controlsList = new List<IControls>();
            }

            if (_MobileInput == null)
            {
                _MobileInput = new MobileInputActions();
            }

            if (GameManager.Instance.Setting != null)
            {
                if (GameManager.Instance.Setting.inputSetting.isTouchable)
                {
                    TouchDetection _touch = gameObject.AddComponent<TouchDetection>();
                    _controlsList.Add(_touch);
                }
                if (GameManager.Instance.Setting.inputSetting.isPinchable)
                {
                    PinchDetection _pinch = gameObject.AddComponent<PinchDetection>();
                    _controlsList.Add(_pinch);
                }

                if (GameManager.Instance.Setting.inputSetting.isSwipable)
                {
                    SwipeDetection _swipe = gameObject.AddComponent<SwipeDetection>();
                    _controlsList.Add(_swipe);
                }
            }
            
            /*
            // Check Device Type That Running The Application
            switch (SystemInfo.deviceType)
            {
                case DeviceType.Desktop:
                    //TODO: Set Desktop Input Setting Environment
                    break;
                case DeviceType.Handheld:
                    _MobileInput = new MobileInputActions();
                    if (Setting.inputSetting.isTouchable)
                    {
                        TouchDetection _touch = gameObject.AddComponent<TouchDetection>();
                        _controlsList.Add(_touch);
                        Logging.InputLogger.Log("TouchDetection Added");
                    }
                    if (Setting.inputSetting.isPinchable)
                    {
                        PinchDetection _pinch = gameObject.AddComponent<PinchDetection>();
                        _controlsList.Add(_pinch);
                        Logging.InputLogger.Log("PinchDetection Added");
                    }

                    if (Setting.inputSetting.isSwipable)
                    {
                        SwipeDetection _swipe = gameObject.AddComponent<SwipeDetection>();
                        _controlsList.Add(_swipe);
                        Logging.InputLogger.Log("SwipeDetection Added");
                    }
                    break;
            }
            */
            
        }

        public void DisCompileControlsInPreviousScene()
        {
            if (_controlsList == null)
            {
                Logging.InputLogger.Log("controlsList is null");
                _controlsList = new List<IControls>();
            }
            else
            {
                Logging.InputLogger.Log("DisCompileControlsList");
                foreach (var control in _controlsList)
                {
                    Destroy(GetComponent(control.GetType()));
                }
            }
        }

        #endregion

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            playerInput.camera = Camera.main;
            Singleton();
            Instance.CompileControlsInScene();
        }

        private void Start()
        {
            GameManager.LoadNewScene += DisCompileControlsInPreviousScene;
        }
    }
}
