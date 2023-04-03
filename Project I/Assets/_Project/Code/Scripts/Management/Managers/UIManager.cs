using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Wonderland.Management
{
    public class UIManager : IManager
    {
        public VisualElement Root;
        public VisualElement CurrentUxml { get; private set; }
        public GameObject defaultCanvas;
        public GameObject loadingScreen;

        #region UXML Managing Methods

        public static event Action UxmlChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUxmlName()
        {
            string currentMainPanel = CurrentUxml.Q<GroupBox>().name;
            return currentMainPanel;
        }
        
        /// <summary>
        /// This method is used to change UXML file of the UIDocument in the scene
        /// Mainly use for SignUp and SignIn interface
        /// </summary>
        /// <param name="newUxml"></param>
        public void ChangeUxml(VisualTreeAsset newUxml)
        {
            ClearCurrentUxml();

            // Build a tree of VisualElement from new VisualTreeAsset and assigned to currentUxml ( VisualElement )
            CurrentUxml = newUxml.CloneTree();
            CurrentUxml.style.position = Position.Relative;
            CurrentUxml.style.height = Screen.safeArea.height;

            // Invoke Any Function that attach to UxmlChanged Event In That Scene
            UxmlChanged?.Invoke();

            // Add currentUxml to the root of UIDocument in the scene
            Root.Add(CurrentUxml);
            Logging.UILogger.Log("ChangeUxml To " + newUxml.name);
        }

        /// <summary>
        /// This method is used to clear the current Uxml in the hierarchy right now
        /// </summary>
        public void ClearCurrentUxml()
        {
            if (Root.Contains(CurrentUxml))
            {
                // Remove the currentUxml from the parent templateContainer
                CurrentUxml.RemoveFromHierarchy();
            }
        }
        
        /// <summary>
        /// Delete Every Uxml Template out of UIDocument's Root
        /// </summary>
        public void ClearUI()
        {
            Root.Clear();
        }

        #endregion

        #region Loading Screen Management
        
        public void HideLoadingScreen()
        {
            loadingScreen.SetActive(false);
        }
        
        public void ShowLoadingScreen()
        {
            ClearUI();
            loadingScreen.SetActive(true);
        }

        #endregion

        private void UpdateInstance()
        {
            MainManager.Instance.UIManager.Root = Root;
            MainManager.Instance.UIManager.defaultCanvas = defaultCanvas;
            MainManager.Instance.UIManager.loadingScreen = loadingScreen;
        }

        private void Awake()
        {
            Root = GameObject.FindWithTag("UIDocument").GetComponent<UIDocument>().rootVisualElement;
            defaultCanvas = GameObject.FindWithTag("UI");
            loadingScreen = defaultCanvas.transform.GetChild(0).gameObject;
            HideLoadingScreen();
            MainManager.BeforeDestroyMainManager += UpdateInstance;
        }

        private void OnDisable()
        {
            MainManager.BeforeDestroyMainManager -= UpdateInstance;
        }
    }
}
