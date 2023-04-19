using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wonderland.Management
{
    public class MainManager : MonoBehaviour
    {
        #region Singleton

        public static MainManager Instance { get; private set; }
        private readonly List<IManager> _managers = new();
        public static event Action OnDestroyMainManager;
        
        private void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Instance.sceneHandler = sceneHandler;
                OnDestroyMainManager?.Invoke();
                Destroy(gameObject);
            }
        }

        private void GetIManagers(Array array)
        {
            foreach (IManager manager in array)
            {
                if (manager.GetComponent<IManager>() == null) return;
                _managers.Add(manager);
            }
        }

        private void SetIManagers(List<IManager> managers)
        {
            foreach (IManager manager in managers)
            {
                switch (manager.GetComponent<IManager>().name)
                {
                    case"FirebaseManager":
                        FirebaseManager = manager.GetComponent<FirebaseManager>();
                        Logging.ManagerLogger.Log(manager.name + "Has Been Assigned");
                        break;
                    case "GameManager":
                        GameManager = manager.GetComponent<GameManager>();
                        Logging.ManagerLogger.Log(manager.name + "Has Been Assigned");
                        break;
                    case "InputManager":
                        InputManager = manager.GetComponent<InputManager>();
                        Logging.ManagerLogger.Log(manager.name + "Has Been Assigned");
                        break;
                    case "UIManager":
                        UIManager = manager.GetComponent<UIManager>();
                        Logging.ManagerLogger.Log(manager.name + "Has Been Assigned");
                        break;
                    case "SoundManager":
                        SoundManager = manager.GetComponent<SoundManager>();
                        Logging.ManagerLogger.Log(manager.name + "Has Been Assigned");
                        break;
                }
            }
        }

        #endregion
        [Header("Setting")] 
        public SceneHandler sceneHandler;
        
        public FirebaseManager FirebaseManager { get; private set; }
        public GameManager GameManager{ get; private set; }
        public InputManager InputManager{ get; private set; }
        public UIManager UIManager{ get; private set; }
        public SoundManager SoundManager{ get; private set; }
        
        private void Awake()
        {
            Logging.LoadLogger();
            GetIManagers(gameObject.GetComponentsInChildren<IManager>());
            SetIManagers(_managers);
        }

        private void OnEnable()
        {
            Singleton();
            Instance.InputManager.EnableInputDetections();
        }
    }
}
