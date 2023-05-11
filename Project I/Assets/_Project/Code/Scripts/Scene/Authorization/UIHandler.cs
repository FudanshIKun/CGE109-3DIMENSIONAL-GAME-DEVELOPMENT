using UnityEngine;
using UnityEngine.UIElements;
using Wonderland.Management;

namespace Wonderland.Scene.Authorization
{
    public class UIHandler : Management.UIHandler
    {
        public static UIHandler Instance;
        
        [Header("UI Assets")]
        [SerializeField] private VisualTreeAsset signUpUxml;
        [SerializeField] private VisualTreeAsset signInUxml;

        public VisualTreeAsset SignUp => signUpUxml;

        #region Components

        private TextField _userNameField;
        private TextField _emailField;
        private TextField _passwordField;
        private TextField _confirmPasswordField;
        private Label _errorText;

        protected override void Awake()
        {
            base.Awake();
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            UxmlChanged += OnUxmlChange;
        }
        
        
        private void OnDisable()
        {
            UxmlChanged -= OnUxmlChange;
        }

        #endregion

        #region Methods

        private void OnClickSignUp() => AuthorizationHandler.SignUp(_userNameField.value, _emailField.value, _passwordField.value, _confirmPasswordField.value, _errorText);

        private void OnClickSignIn() => AuthorizationHandler.SignIn(_emailField.value, _passwordField.value, _errorText);

        private void OnClickContinue() => AuthorizationHandler.ContinueAsGuest();
        
        private void LoadSignUpTab()
        {
            ChangeUxml(signUpUxml);
        }
        
        private void LoadSignInTab()
        {
            ChangeUxml(signInUxml);
        }

        private void OnUxmlChange()
        {
            var continueButton = CurrentUxml.Q<Button>("ContinueAsGuest");
            continueButton.clicked += OnClickContinue;
            
            switch (GetCurrentUxmlName())
            {

                case "SignUpPanel":
                    _userNameField = CurrentUxml.Q<TextField>("UserName-Field");
                    _emailField = CurrentUxml.Q<TextField>("Email-Field");
                    _passwordField = CurrentUxml.Q<TextField>("Password-Field");
                    _confirmPasswordField = CurrentUxml.Q<TextField>("Confirm-Field");
                    _errorText = CurrentUxml.Q<Label>("ErrorOutput");
                    var signUpButton = CurrentUxml.Q<Button>("Authentication-Button");
                    signUpButton.clicked += OnClickSignUp;
                    var signInTab = CurrentUxml.Q<Button>("SignIn");
                    signInTab.clicked += LoadSignInTab;
                    break;
                case "SignInPanel":
                    _emailField = CurrentUxml.Q<TextField>("Email-Field");
                    _passwordField = CurrentUxml.Q<TextField>("Password-Field");
                    _errorText = CurrentUxml.Q<Label>("ErrorOutput");
                    var signInButton = CurrentUxml.Q<Button>("Authentication-Button");
                    signInButton.clicked += OnClickSignIn;
                    var signUpTab = CurrentUxml.Q<Button>("SignUp");
                    signUpTab.clicked += LoadSignUpTab;
                    break;
            }
        }

        #endregion
    }
}