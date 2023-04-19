using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Wonderland.Management
{
    public class JoyStickDetection : Controls
    {
        public static event Action OnMoving;
        public static event Action OnStopMove;
        public static Vector2 MovementDirection { get; private set; }
        public static Vector2 MovementAmount { get; private set; }
        public static bool IsMoving { get; private set; }
        public static event Action OnAiming;
        public static event Action OnStopAim;
        public static Vector2 AimDirection { get; private set; }
        public static Vector2 AimAmount { get; private set; }
        public static bool IsAiming { get; private set; }
        public static JoystickController Controller { get; set; }

        private void Start()
        {
            Controller.LeftStick.OnPerform += OnMoving;
            Controller.LeftStick.OnStop += OnStopMove;
            Controller.RightStick.OnPerform += OnAiming;
            Controller.RightStick.OnStop += OnStopAim;
        }

        private void Update()
        {
            MovementDirection = Controller.LeftStick.Direction;
            MovementAmount = new Vector2(Controller.LeftStick.Horizontal, Controller.LeftStick.Vertical);
            IsMoving = MovementAmount != Vector2.zero;

            AimDirection = Controller.RightStick.Direction;
            AimAmount = new Vector2(Controller.RightStick.Horizontal, Controller.RightStick.Vertical);
            IsAiming = AimAmount != Vector2.zero;

            #region Logging

            Logging.InputSystemLogger.Log(MovementAmount != Vector2.zero
                ? "MovementFinger Is Active" : "MovementFinger Is Null");

            Logging.InputSystemLogger.Log(AimAmount != Vector2.zero
                ? "AimFinger Is Active" : "AimFinger Is Null");
            
            Logging.InputSystemLogger.Log("IsAiming: " + IsAiming);
            
            #endregion
        }
    }
}
