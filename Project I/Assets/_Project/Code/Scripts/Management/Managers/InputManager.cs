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
        private static HandheldInputAction HandheldInputAction { get; set; }
        private List<Controls> ControlsList { get; set; }

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
        public void EnableInputDetectionInScene()
        {
            //
            if (ControlsList == null)
            {
                ControlsList = new List<Controls>();
            }

            //
            if (HandheldInputAction == null)
            {
                HandheldInputAction = new HandheldInputAction();
            }

            if (MainManager.Setting != null)
            {
                //
                if (MainManager.Setting.inputSetting.Touchable)
                {
                    if (GetComponent<TouchDetection>() == null)
                    {
                        TouchDetection touch = gameObject.AddComponent<TouchDetection>();
                        touch.enabled = true;
                        ControlsList.Add(touch);
                    }
                    else
                    {
                        TouchDetection touch = gameObject.GetComponent<TouchDetection>();
                        touch.enabled = true;
                        ControlsList.Add(touch);
                    }
                }

                //
                if (MainManager.Setting.inputSetting.Swipable)
                {
                    if (GetComponent<SwipeDetection>() == null)
                    {
                        SwipeDetection swipe = gameObject.AddComponent<SwipeDetection>();
                        swipe.enabled = true;
                        ControlsList.Add(swipe);
                    }
                    else
                    {
                        SwipeDetection swipe = gameObject.GetComponent<SwipeDetection>();
                        swipe.enabled = true;
                        ControlsList.Add(swipe);
                    } ;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisableInputDetectionFromPreviousScene()
        {
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
        
        private void StartTouchPrimary(InputAction.CallbackContext context)
        {
            if (OnStartPrimaryTouchEvent != null) OnStartPrimaryTouchEvent(Utils.ScreenToCamera(mainCamera, HandheldInputAction.Touch.PrimaryTouchValue.ReadValue<Vector2>()), Utils.ScreenToObject(mainCamera,HandheldInputAction.Touch.PrimaryTouchValue.ReadValue<Vector2>()), (float)context.time);
        }
        
        private void EndTouchPrimary(InputAction.CallbackContext context)
        {
            if (OnEndPrimaryTouchEvent != null)
            {
                OnEndPrimaryTouchEvent(Utils.ScreenToCamera(mainCamera, HandheldInputAction.Touch.PrimaryTouchValue.ReadValue<Vector2>()), Utils.ScreenToObject(mainCamera,HandheldInputAction.Touch.PrimaryTouchValue.ReadValue<Vector2>()), (float)context.time);
            }
        }

        #endregion

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
            MainManager.BeforeDestroyMainManager += UpdateInstance;
        }

        private void Start()
        {
            //
            GameManager.LoadNewScene += DisableInputDetectionFromPreviousScene;
            
            //
            HandheldInputAction.Enable();
            HandheldInputAction.Touch.PrimaryTouchContact.started += context => StartTouchPrimary(context);
            HandheldInputAction.Touch.PrimaryTouchContact.canceled += context => EndTouchPrimary(context);
        }

        private void OnDisable()
        {
            HandheldInputAction.Disable();
            MainManager.BeforeDestroyMainManager -= UpdateInstance;
        }
    }
}
