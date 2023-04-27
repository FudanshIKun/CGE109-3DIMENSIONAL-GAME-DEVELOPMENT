using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Wonderland.Management
{
    public class UIManager : Manager
    {
        [Header("Settings")]
        [SerializeField] private Canvas defaultCanvas;
        [SerializeField] private UIDocument uiDocument;
        public JoystickController joystickController;
        
        public GameObject UICanvas { get; private set; }
        private VisualElement Root { get; set; }
        public VisualElement CurrentUxml { get; private set; }
        private GameObject LoadingScreen { get; set; }

        private void UpdateInstance()
        {
            MainManager.Instance.UIManager.UICanvas = UICanvas;
            MainManager.Instance.UIManager.Root = Root;
            MainManager.Instance.UIManager.defaultCanvas = defaultCanvas;
            MainManager.Instance.UIManager.LoadingScreen = LoadingScreen;
        }

        private void Awake()
        {
            UICanvas = GameObject.FindWithTag("UI");
            Root = uiDocument.rootVisualElement;
            LoadingScreen = defaultCanvas.transform.GetChild(0).gameObject;
            joystickController.gameObject.SetActive(false);
            HideLoadingScreen();
            MainManager.OnDestroyMainManager += UpdateInstance;
        }

        private void OnDisable()
        {
            MainManager.OnDestroyMainManager -= UpdateInstance;
        }
        
        #region Methods

        public static event Action UxmlChanged;
        
        public string GetCurrentUxmlName()
        {
            var currentUxmlName = CurrentUxml.Q<GroupBox>().name;
            return currentUxmlName;
        }
        
        public void ChangeUxml(VisualTreeAsset newUxml)
        {
            ClearCurrentUxml();
            
            CurrentUxml = newUxml.CloneTree();
            CurrentUxml.style.position = Position.Relative;
            CurrentUxml.style.height = Screen.safeArea.height;
            
            UxmlChanged?.Invoke();
            
            Root.Add(CurrentUxml);
            Logging.UILogger.Log("ChangeUxml To " + newUxml.name);
        }
        
        public void ClearCurrentUxml()
        {
            if (Root.Contains(CurrentUxml))
            {
                // Remove the currentUxml from the parent templateContainer
                CurrentUxml.RemoveFromHierarchy();
            }
        }

        public void HideLoadingScreen()
        {
            LoadingScreen.SetActive(false);
        }
        
        public void ShowLoadingScreen()
        {
            Root.Clear();
            LoadingScreen.SetActive(true);
        }

        #endregion
    }
}
