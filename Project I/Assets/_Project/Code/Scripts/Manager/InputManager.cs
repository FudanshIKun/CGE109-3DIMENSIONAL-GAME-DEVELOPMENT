using UnityEngine;
using UnityEngine.InputSystem;

namespace Wonderland.Manager
{
    public class InputManager : MonoBehaviour
    {
        #region Singleton

        public static InputManager Instance;

        private void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Instance._playerInput = _playerInput;
                Destroy(gameObject);
            }
        }

        #endregion

        private PlayerInput _playerInput;
        
        #region Methods

        

        #endregion

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            Singleton();
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            Debug.Log(_playerInput.actions.name);
        }

        private void OnDisable()
        {
            
        }
    }
}
