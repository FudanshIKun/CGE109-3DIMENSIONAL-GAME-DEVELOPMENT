using UnityEngine;
using Joysticks;

namespace Wonderland.Client.MainWorld
{
    public class Controller : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private DragArea dragArea;
        [SerializeField] private FloatingJoystick leftStick;
        [SerializeField] private FixedJoystick rightStick;

        public DragArea DragArea
        {
            get => dragArea;
            private set => dragArea = value;
        }
        
        public FloatingJoystick LeftStick
        {
            get => leftStick;
            private set => leftStick = value;
        }

        public FixedJoystick RightStick
        {
            get => rightStick;
            private set => rightStick = value;
        }

        private void Awake()
        {
            DragArea = dragArea;
            LeftStick = leftStick;
            RightStick = rightStick;
        }
    }
}
