using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Wonderland.Management;

namespace Wonderland.Client.MainWorld
{
    public class InputHandler : SerializedMonoBehaviour
    {
        public static InputHandler Instance;

        [Header("Settings")] [Header("Required Components")] 
        [SerializeField] private Controller controller;
        
        private static bool Active { get; set; }
        
        public Vector2 Movement { get; private set; }
        public Vector2 Aiming { get; private set; }
        public Vector2 Dragging { get; private set; }
        public static bool IsMoving { get; private set; }
        public static bool IsAiming { get; private set; }
        public static bool IsDragging { get; private set; }
        public static Controller Controller { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Controller = Instance.controller;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (!Active) return;
            
            IsDragging = Controller.DragArea.DragAmount != Vector2.zero;
            if (IsDragging) Dragging = Controller.DragArea.DragAmount;
            
            Movement = new Vector2(Controller.LeftStick.Horizontal, Controller.LeftStick.Vertical);
            IsMoving = Movement != Vector2.zero;

            IsAiming = Controller.RightStick.IsPressing;
            Aiming = Controller.RightStick.DragAmount;
        }

        #region Methods

        public static void Disable()
        {
            CustomLog.Manager.Log("DisableInput");
            Active = false;
            Controller.gameObject.SetActive(false);
        }

        public static void Enable()
        {
            CustomLog.Manager.Log("EnableInput");
            Active = true;
            Controller.gameObject.SetActive(true);
        }

        #endregion
    }
}