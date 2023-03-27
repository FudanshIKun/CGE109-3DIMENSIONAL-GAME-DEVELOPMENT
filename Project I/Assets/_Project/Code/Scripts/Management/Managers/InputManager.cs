using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Wonderland.InputActions;


namespace Wonderland.Management
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : IManager
    {
        #region Fields

        [HideInInspector] public PlayerInput playerInput;
        [HideInInspector] public Camera mainCamera;
        private static HandheldInputAction _handheldInputAction;
        private List<Controls> _controlsList;

        #endregion
        
        #region Input Events

        // Start Primary Touch Event
        public delegate void StartPrimaryTouch(Vector2 position, GameObject interacted, float time);
        public event StartPrimaryTouch OnStartPrimaryTouchEvent;
        
        // End Primary Touch Event
        public delegate void EndPrimaryTouch(Vector2 position, GameObject interacted, float time);
        public event EndPrimaryTouch OnEndPrimaryTouchEvent;

        #endregion

        #region Methods
        
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
        public void CompileControlsInScene()
        {
            //
            if (_controlsList == null)
            {
                _controlsList = new List<Controls>();
            }

            //
            if (_handheldInputAction == null)
            {
                _handheldInputAction = new HandheldInputAction();
            }

            if (MainManager.Setting != null)
            {
                //
                if (MainManager.Setting.inputSetting.touchable)
                {
                    if (GetComponent<TouchDetection>() == null)
                    {
                        TouchDetection touch = gameObject.AddComponent<TouchDetection>();
                        touch.enabled = true;
                        _controlsList.Add(touch);
                    }
                    else
                    {
                        TouchDetection touch = gameObject.GetComponent<TouchDetection>();
                        touch.enabled = true;
                        _controlsList.Add(touch);
                    }
                }

                //
                if (MainManager.Setting.inputSetting.swipable)
                {
                    if (GetComponent<SwipeDetection>() == null)
                    {
                        SwipeDetection swipe = gameObject.AddComponent<SwipeDetection>();
                        swipe.enabled = true;
                        _controlsList.Add(swipe);
                    }
                    else
                    {
                        SwipeDetection swipe = gameObject.GetComponent<SwipeDetection>();
                        swipe.enabled = true;
                        _controlsList.Add(swipe);
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
                _controlsList = new List<Controls>();
            }
            else
            {
                foreach (var control in _controlsList)
                {
                    control.enabled = false;
                }
            }
        }
        
        private void StartTouchPrimary(InputAction.CallbackContext context)
        {
            if (OnStartPrimaryTouchEvent != null) OnStartPrimaryTouchEvent(Utils.ScreenToCamera(mainCamera, _handheldInputAction.Touch.PrimaryTouchValue.ReadValue<Vector2>()), Utils.ScreenToObject(mainCamera,_handheldInputAction.Touch.PrimaryTouchValue.ReadValue<Vector2>()), (float)context.time);
        }
        
        private void EndTouchPrimary(InputAction.CallbackContext context)
        {
            if (OnEndPrimaryTouchEvent != null)
            {
                OnEndPrimaryTouchEvent(Utils.ScreenToCamera(mainCamera, _handheldInputAction.Touch.PrimaryTouchValue.ReadValue<Vector2>()), Utils.ScreenToObject(mainCamera,_handheldInputAction.Touch.PrimaryTouchValue.ReadValue<Vector2>()), (float)context.time);
            }
        }

        #endregion

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            playerInput.camera = Camera.main;
            mainCamera = playerInput.camera;
        }

        private void Start()
        {
            //
            GameManager.LoadNewScene += DisCompileControlsInPreviousScene;
            
            //
            _handheldInputAction.Enable();
            _handheldInputAction.Touch.PrimaryTouchContact.started += context => StartTouchPrimary(context);
            _handheldInputAction.Touch.PrimaryTouchContact.canceled += context => EndTouchPrimary(context);
        }

        private void OnDisable()
        {
            _handheldInputAction.Disable();
        }
    }
}
