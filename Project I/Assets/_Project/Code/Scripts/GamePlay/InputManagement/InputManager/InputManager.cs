using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Wonderland.InputActions;
using Wonderland.Manager;
using Wonderland.Utility;


namespace Wonderland.GamePlay.InputManagement
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
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
                Instance._playerInput = _playerInput;
                Instance._mainCamera = _mainCamera;
                Destroy(gameObject);
            }
        }

        #endregion

        #region Input Events

        // Start Primary Touch Event
        public delegate void StartPrimaryTouch(Vector2 position, GameObject interacted, float time);
        public event StartPrimaryTouch OnStartPrimaryTouchEvent;
        
        // End Primary Touch Event
        public delegate void EndPrimaryTouch(Vector2 position, GameObject interacted, float time);
        public event EndPrimaryTouch OnEndPrimaryTouchEvent;

        #endregion

        #region Input Fields

        private PlayerInput _playerInput;
        private Camera _mainCamera;
        public MobileInputActions _MobileInput;
        private List<IControls> _controlsList;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public void CheckDeviceType()
        {
            // Check Device Type That Running The Application
            switch (SystemInfo.deviceType)
            {
                case DeviceType.Desktop:
                    //TODO: Set Desktop Input Setting Environment
                    break;
                case DeviceType.Handheld:
                    //TODO: Set Handheld Input Setting Environment
                    break;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void CompileControlsInScene()
        {
            //
            if (_controlsList == null)
            {
                _controlsList = new List<IControls>();
            }

            //
            if (_MobileInput == null)
            {
                _MobileInput = new MobileInputActions();
            }

            if (GameManager.Instance.Setting != null)
            {
                //
                if (GameManager.Instance.Setting.inputSetting.isTouchable)
                {
                    if (GetComponent<TouchDetection>() == null)
                    {
                        TouchDetection _touch = gameObject.AddComponent<TouchDetection>();
                        _touch.enabled = true;
                        _controlsList.Add(_touch);
                    }
                    else
                    {
                        TouchDetection _touch = gameObject.GetComponent<TouchDetection>();
                        _touch.enabled = true;
                        _controlsList.Add(_touch);
                    }
                }

                //
                if (GameManager.Instance.Setting.inputSetting.isSwipable)
                {
                    if (GetComponent<SwipeDetection>() == null)
                    {
                        SwipeDetection _swipe = gameObject.AddComponent<SwipeDetection>();
                        _swipe.enabled = true;
                        _controlsList.Add(_swipe);
                    }
                    else
                    {
                        SwipeDetection _swipe = gameObject.GetComponent<SwipeDetection>();
                        _swipe.enabled = true;
                        _controlsList.Add(_swipe);
                    } ;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisCompileControlsInPreviousScene()
        {
            if (_controlsList == null)
            {
                _controlsList = new List<IControls>();
            }
            else
            {
                foreach (var control in _controlsList)
                {
                    control.enabled = false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private void StartTouchPrimary(InputAction.CallbackContext context)
        {
            if (OnStartPrimaryTouchEvent != null) OnStartPrimaryTouchEvent(Utils.ScreenToCamera(_mainCamera, _MobileInput.Touch.PrimaryTouchValue.ReadValue<Vector2>()), Utils.ScreenToObject(_mainCamera,_MobileInput.Touch.PrimaryTouchValue.ReadValue<Vector2>()), (float)context.time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private void EndTouchPrimary(InputAction.CallbackContext context)
        {
            if (OnEndPrimaryTouchEvent != null)
            {
                OnEndPrimaryTouchEvent(Utils.ScreenToCamera(_mainCamera, _MobileInput.Touch.PrimaryTouchValue.ReadValue<Vector2>()), Utils.ScreenToObject(_mainCamera,_MobileInput.Touch.PrimaryTouchValue.ReadValue<Vector2>()), (float)context.time);
            }
        }

        #endregion

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.camera = Camera.main;
            _mainCamera = _playerInput.camera;
            Singleton();
            Instance.CompileControlsInScene();
        }

        private void OnEnable()
        {
            _MobileInput?.Enable();
        }

        private void Start()
        {
            //
            GameManager.LoadNewScene += DisCompileControlsInPreviousScene;
            
            //
            _MobileInput.Touch.PrimaryTouchContact.started += context => StartTouchPrimary(context);
            _MobileInput.Touch.PrimaryTouchContact.canceled += context => EndTouchPrimary(context);
        }

        private void OnDisable()
        {
            _MobileInput?.Disable();
        }
    }
}
