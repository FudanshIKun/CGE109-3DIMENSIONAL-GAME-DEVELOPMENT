using UnityEngine;
using UnityEngine.UIElements;
using Wonderland.Management;

namespace Wonderland.GamePlay.Authorization
{
    public class UIHandler : Management.UIHandler
    {
        [Header("UI Assets")]
        [SerializeField] VisualTreeAsset signUpUxml;
        [SerializeField] VisualTreeAsset signInUxml;

        #region Components

        private TextField _userNameField;
        private TextField _emailField;
        private TextField _passwordField;
        private TextField _confirmPasswordField;
        private Label _errorText;

        #endregion

        #region Methods

        private void OnClickSignUp() => FirebaseManager.SignUpAsync(_userNameField.value, _emailField.value, _passwordField.value, _confirmPasswordField.value, _errorText);

        private void OnClickSignIn() => FirebaseManager.SignInAsync(_emailField.value, _passwordField.value, _errorText);
        
        private void LoadSignUpTab()
        {
            MainManager.Instance.UIManager.ChangeUxml(signUpUxml);
        }
        
        private void LoadSignInTab()
        {
            MainManager.Instance.UIManager.ChangeUxml(signInUxml);
        }
        
        protected override void OnUxmlChange()
        {
            var currentUxml = MainManager.Instance.UIManager.CurrentUxml;
            switch (MainManager.Instance.UIManager.GetCurrentUxmlName())
            {
                case "SignUpPanel":
                    _userNameField = currentUxml.Q<TextField>("UserName-Field");
                    _emailField = currentUxml.Q<TextField>("Email-Field");
                    _passwordField = currentUxml.Q<TextField>("Password-Field");
                    _confirmPasswordField = currentUxml.Q<TextField>("Confirm-Field");
                    _errorText = currentUxml.Q<Label>("ErrorOutput");
                    Button signUpButton = currentUxml.Q<Button>("Authentication-Button");
                    signUpButton.clicked += OnClickSignUp;
                    Button signInTab = currentUxml.Q<Button>("SignIn");
                    signInTab.clicked += LoadSignInTab;
                    break;
                case "SignInPanel":
                    _emailField = currentUxml.Q<TextField>("Email-Field");
                    _passwordField = currentUxml.Q<TextField>("Password-Field");
                    _errorText = currentUxml.Q<Label>("ErrorOutput");
                    Button signInButton = currentUxml.Q<Button>("Authentication-Button");
                    signInButton.clicked += OnClickSignIn;
                    Button signUpTab = currentUxml.Q<Button>("SignUp");
                    signUpTab.clicked += LoadSignUpTab;
                    break;
            }
        }

        #endregion
    }
}
