using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Wonderland.Manager
{
    public class UIManager : MonoBehaviour
    {
        #region Singleton

        public static UIManager Instance;
        private void Singleton()
        {
            _root = GameObject.FindWithTag("UI").GetComponent<UIDocument>().rootVisualElement;
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Instance._root = _root;
                Destroy(gameObject);
            }
        }

        #endregion
        
        private VisualElement _root;
        private VisualElement currentUxml;
        
        [Header("")] [Tooltip("")]
        public VisualTreeAsset SignUpUXML;
        public VisualTreeAsset SignInUXML;
        #region Authentication Components

        public TextField userField;
        public TextField emailField;
        public TextField passwordField;
        public TextField confirmField;
        public RadioButton rememberPassword;
        public Label errorOutput;

        #endregion

        [Header("Scene Loader UI Assets")] 
        public VisualTreeAsset LoadingUXML;

        [Header("Lobby UI Assets")]
        public VisualTreeAsset LobbyUXML;

        #region UXML Managements

        /// <summary>
        /// This method is used to change UXML file of the UIDocument in the scene
        /// Mainly use for SignUp and SignIn interface
        /// </summary>
        /// <param name="new UXML"></param>
        public void ChangeUxml(VisualTreeAsset newUXML)
        {
            if (_root.Contains(currentUxml))
            {
                // Remove the currentUxml from the parent templateContainer
                currentUxml.RemoveFromHierarchy();
            }
            // Build a tree of VisualElement from new VisualTreeAsset and assigned to currentUxml ( VisualElement )
            currentUxml = newUXML.CloneTree();
            currentUxml.style.position = Position.Relative;
            currentUxml.style.height = Screen.safeArea.height;

            // Check If The Loaded Uxml Is Authentication UI Type
            if (currentUxml.Q<Label>("Authentication-Type") != null)
            {
                switch (currentUxml.Q<Label>("Authentication-Type").text)
                {
                    case "New Account":
                        userField = currentUxml.Q<TextField>("User-Field");
                        emailField = currentUxml.Q<TextField>("Email-Field");
                        passwordField = currentUxml.Q<TextField>("Password-Field");
                        confirmField = currentUxml.Q<TextField>("Confirm-Field");
                        rememberPassword = currentUxml.Q<RadioButton>("RememberPassowrd");
                        errorOutput = currentUxml.Q<Label>("ErrorOutput");
                        var signUpButton = currentUxml.Q<Button>("Authentication-Button");
                        signUpButton.clicked += FirebaseManager.Instance.SignUp;
                        break;
                    case "Sign In":
                        emailField = currentUxml.Q<TextField>("Email-Field");
                        passwordField = currentUxml.Q<TextField>("Password-Field");
                        rememberPassword = currentUxml.Q<RadioButton>("RememberPassword");
                        errorOutput = currentUxml.Q<Label>("ErrorOutput");
                        var signInButton = currentUxml.Q<Button>("Authentication-Button");
                        signInButton.clicked += FirebaseManager.Instance.SignIn;
                        break;
                }
            }

            // Add currentUxml to the root of UIDocument in the scene
            _root.Insert(0,currentUxml);
            
            //Debug.LogFormat("Change currentUxml to {0}", newUXML);
        }

        #endregion

        private void Awake()
        {
            Singleton();
        }

        private void Start()
        {
            
        }
    }
}
