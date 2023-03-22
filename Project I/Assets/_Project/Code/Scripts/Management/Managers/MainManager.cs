using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Wonderland.API;

namespace Wonderland.Management
{
    public class MainManager : MonoBehaviour
    {
        #region Fields
        
        #region Managers

        [Header("Managers")]
        public GameManager gameManager;
        public InputManager inputManager;
        public UIManager uiManager;
        
        #endregion

        #region Handlers
        
        public static GameSceneHandler GameSceneHandler;
        public static GamePlayHandler GamePlayHandler;
        public static SceneHandler SceneHandler;
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
        
        private void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                UpdateInstance();
                Destroy(gameObject);
            }
        }

        private void UpdateInstance()
        {
            Logging.ManagerLogger.Log("Instance Updated");
            GameManager.CurrentScene = gameManager.setSceneType;
            Instance.inputManager.playerInput = inputManager.playerInput;
            Instance.inputManager.mainCamera = inputManager.mainCamera;
            Instance.uiManager.Root = uiManager.Root;
            Instance.uiManager.defaultCanvas = uiManager.defaultCanvas;
            Instance.uiManager.loadingScreen = uiManager.loadingScreen;
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
                    case "GameManager":
                        gameManager = manager.GetComponent<GameManager>();
                        Logging.ManagerLogger.Log(manager.name + "Has Been Assigned");
                        break;
                    case "InputManager":
                        inputManager = manager.GetComponent<InputManager>();
                        Logging.ManagerLogger.Log(manager.name + "Has Been Assigned");
                        break;
                    case "UIManager":
                        uiManager = manager.GetComponent<UIManager>();
                        Logging.ManagerLogger.Log(manager.name + "Has Been Assigned");
                        break;
                }
            }
        }

        #endregion

        #region Methods

        public IEnumerator WaitForTask(Task task)
        {
            yield return new WaitUntil(() => task.IsCompleted);
        }

        public async Task<Task> WaitForTaskAsync(Task task)
        {
            await Task.Run(async () =>
            {
                while (task.IsCompleted) await Task.Delay(100);
            });
            
            return task;
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
            Instance.inputManager.CompileControlsInScene();
        }

        private void OnApplicationQuit()
        {
            AuthAPI.SignOutRequest();
        }
    }
}
