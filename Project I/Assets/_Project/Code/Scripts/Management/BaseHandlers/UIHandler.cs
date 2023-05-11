using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace Wonderland.Management
{
    public abstract class UIHandler : Handler
    {
        [Header("Settings")] 
        [Header("Required Components")]
        [SerializeField] private Transform parent;
        [SerializeField] private UIDocument uiDocument;
        [SerializeField] private Canvas defaultCanvas;
        [SerializeField] private GameObject loadingScreen;

        protected static VisualElement Root { get; set; }
        protected static VisualElement CurrentUxml { get; set; }
        protected static GameObject LoadingScreen { get; set; }
        
        public static event Action UxmlChanged;

        protected virtual void Awake()
        {
            parent = GameObject.FindWithTag("UI").transform;
            Root = uiDocument.rootVisualElement;
        }

        private void Start()
        {
            SetLoadingScreen(loadingScreen);
            HideLoadingScreen();
        }

        #region Methods

        protected static void SetLoadingScreen(GameObject loadingScreen)
        {
            if (loadingScreen != null)
            {
                LoadingScreen = loadingScreen;
            }
            else
            {
                
            }
        }

        public static string GetCurrentUxmlName()
        {
            var currentUxmlName = CurrentUxml.Q<GroupBox>().name;
            return currentUxmlName;
        }
        
        public static void ChangeUxml(VisualTreeAsset newUxml)
        {
            ClearCurrentUxml();
            
            CurrentUxml = newUxml.CloneTree();
            CurrentUxml.style.position = Position.Relative;
            CurrentUxml.style.height = Screen.safeArea.height;
            
            UxmlChanged?.Invoke();
            
            Root.Add(CurrentUxml);
            CustomLog.UI.Log("ChangeUxml To " + newUxml.name);
        }
        
        public static void ClearCurrentUxml()
        {
            if (Root.Contains(CurrentUxml))
            {
                // Remove the currentUxml from the parent templateContainer
                CurrentUxml.RemoveFromHierarchy();
            }
        }

        public static void HideLoadingScreen()
        {
            LoadingScreen.SetActive(false);
        }
        
        public static void ShowLoadingScreen()
        {
            Root.Clear();
            LoadingScreen.SetActive(true);
        }

        #endregion
    }
}