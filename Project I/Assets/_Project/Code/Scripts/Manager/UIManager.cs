using System;
using UnityEngine;
using UnityEngine.UIElements;
using Wonderland.Utility;

namespace Wonderland.Manager
{
    public class UIManager : MonoBehaviour
    {
        #region Singleton

        public static UIManager Instance;

        private void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Instance._root = _root;
                Instance.defaultCanvas = defaultCanvas;
                Instance.loadingScreen = loadingScreen;
                Destroy(gameObject);
            }
        }

        #endregion

        private VisualElement _root;
        private VisualElement _currentUxml;
        public VisualElement currentUxml
        {
            get { return _currentUxml; }
            set { _currentUxml = value; }
        }
        private GameObject defaultCanvas;
        private GameObject loadingScreen;

        #region UXML Managements

        public static event Action UxmlChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUxmlName()
        {
            string currentMainPanel = currentUxml.Q<GroupBox>().name;
            return currentMainPanel;
        }
        
        /// <summary>
        /// This method is used to change UXML file of the UIDocument in the scene
        /// Mainly use for SignUp and SignIn interface
        /// </summary>
        /// <param name="new UXML"></param>
        public void ChangeUxml(VisualTreeAsset newUXML)
        {
            if (_root.Contains(_currentUxml))
            {
                // Remove the currentUxml from the parent templateContainer
                _currentUxml.RemoveFromHierarchy();
            }

            // Build a tree of VisualElement from new VisualTreeAsset and assigned to currentUxml ( VisualElement )
            _currentUxml = newUXML.CloneTree();
            _currentUxml.style.position = Position.Relative;
            _currentUxml.style.height = Screen.safeArea.height;

            // Invoke Any Function that attach to UxmlChanged Event In That Scene
            UxmlChanged?.Invoke();

            // Add currentUxml to the root of UIDocument in the scene
            _root.Add(_currentUxml);
        }

        /// <summary>
        /// This method is used to clear the current Uxml in the hierarchy right now
        /// </summary>
        public void ClearCurrentUxml()
        {
            if (_root.Contains(_currentUxml))
            {
                // Remove the currentUxml from the parent templateContainer
                _currentUxml.RemoveFromHierarchy();
            }
        }
        
        /// <summary>
        /// Delete Every Uxml Template out of UIDocument's Root
        /// </summary>
        public void ClearUI()
        {
            _root.Clear();
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

        private void Awake()
        {
            _root = GameObject.FindWithTag("UIDocument").GetComponent<UIDocument>().rootVisualElement;
            defaultCanvas = GameObject.FindWithTag("UI");
            loadingScreen = defaultCanvas.transform.GetChild(0).gameObject;
            HideLoadingScreen();
            Singleton();
        }
    }
}
