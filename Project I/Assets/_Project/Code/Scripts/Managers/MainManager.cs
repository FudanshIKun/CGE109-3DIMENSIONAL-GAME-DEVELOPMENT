using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Wonderland
{
    public class MainManager : MonoBehaviour
    {
        #region Singleton

        public static MainManager Instance;
        private List<IManager> _managers = new List<IManager>();

        public GameManager gameManager;
        public InputManager inputManager;
        public UIManager uiManager;
        
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
            Instance.gameManager.currentScene = gameManager.currentScene;
            Instance.gameManager.Setting = gameManager.Setting;
            Instance.inputManager._playerInput = inputManager._playerInput;
            Instance.inputManager._mainCamera = inputManager._mainCamera;
            Instance.uiManager._root = uiManager._root;
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
    }
}
