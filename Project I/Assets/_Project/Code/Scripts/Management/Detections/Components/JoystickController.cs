using UnityEngine;
using Joysticks;

namespace Wonderland.Management
{
    public class JoystickController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private FloatingJoystick leftStick;
        [SerializeField] private FloatingJoystick rightStick;

        public FloatingJoystick LeftStick
        {
            get => leftStick;
            private set => leftStick = value;
        }

        public FloatingJoystick RightStick
        {
            get => rightStick;
            private set => rightStick = value;
        }
        
        private void Awake()
        {
            LeftStick = leftStick;
            RightStick = rightStick;
        }
    }
}
