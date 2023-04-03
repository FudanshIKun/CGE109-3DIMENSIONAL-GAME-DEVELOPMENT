using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Wonderland.API;

namespace Wonderland.Management
{
    public class MainManager : MonoBehaviour
    {
        #region Fields
        
        #region Managers
        
        public FirebaseManager FirebaseManager { get; private set; }
        public GameManager GameManager{ get; private set; }
        public InputManager InputManager{ get; private set; }
        public UIManager UIManager{ get; private set; }
        public SoundManager SoundManager{ get; private set; }
        
        #endregion

        #region Handlers
        
        public static SceneHandler SceneHandler;
        public static PlayerHandler PlayerHandler;
        public static GameplayHandler GamePlayHandler;
        public static UIHandler UIHandler;

        #endregion
        
        #region Settings

        [Header("Setting")] 
        public static SceneSetting Setting;

        #endregion

        #endregion
        
        #region Singleton

        public static MainManager Instance;
        private readonly List<IManager> _managers = new List<IManager>();
        public static event Action BeforeDestroyMainManager;
        
        private void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                BeforeDestroyMainManager?.Invoke();
                Destroy(gameObject);
            }
        }

        private void GetIManagers(Array array)
        {
            foreach (IManager manager in array)
            {
                if (manager.GetComponent<IManager>() != null)
                {
                    _managers.Add(manager);
                    Logging.ManagerLogger.Log(manager.name + "Has Been Added To List");
                }
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
        private void Awake()
        {
            Logging.LoadLogger();
            GetIManagers(gameObject.GetComponentsInChildren<IManager>());
            SetIManagers(_managers);
        }

        private void OnEnable()
        {
            Singleton();
            Instance.InputManager.EnableInputDetectionInScene();
        }
    }
}
