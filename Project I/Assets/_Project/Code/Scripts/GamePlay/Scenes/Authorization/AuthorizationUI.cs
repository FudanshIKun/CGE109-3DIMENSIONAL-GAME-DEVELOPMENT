using UnityEngine;
using UnityEngine.UIElements;
using Wonderland.Management;

namespace Wonderland.GamePlay.Authorization
{
    public class AuthorizationUI : UIHandler
    {
        [Header("UI Assets")]
        public VisualTreeAsset signUpUxml;
        public VisualTreeAsset signInUxml;

        #region Authentication Components

        public TextField UserNameField;
        public TextField EmailField;
        public TextField PasswordField;
        public TextField ConfirmPasswordField;
        public Label ErrorText;

        #endregion

        private void OnClickSignUp() => AuthenticationHandler.SignUpAsync(UserNameField.value, EmailField.value, PasswordField.value, ConfirmPasswordField.value, ErrorText);

        private void OnClickSignIn() => AuthenticationHandler.SignInAsync(EmailField.value, PasswordField.value, ErrorText);
        
        private void LoadSignUpTab()
        {
            MainManager.Instance.uiManager.ChangeUxml(signUpUxml);
        }
        
        private void LoadSignInTab()
        {
            MainManager.Instance.uiManager.ChangeUxml(signInUxml);
        }
        private void OnUxmlChange()
        {
            VisualElement currentUxml = MainManager.Instance.uiManager.CurrentUxml;
            switch (MainManager.Instance.uiManager.GetCurrentUxmlName())
            {
                case "SignUpPanel":
                    UserNameField = currentUxml.Q<TextField>("UserName-Field");
                    EmailField = currentUxml.Q<TextField>("Email-Field");
                    PasswordField = currentUxml.Q<TextField>("Password-Field");
                    ConfirmPasswordField = currentUxml.Q<TextField>("Confirm-Field");
                    ErrorText = currentUxml.Q<Label>("ErrorOutput");
                    Button signUpButton = currentUxml.Q<Button>("Authentication-Button");
                    signUpButton.clicked += OnClickSignUp;
                    Button SignInTab = currentUxml.Q<Button>("SignIn");
                    SignInTab.clicked += LoadSignInTab;
                    break;
                case "SignInPanel":
                    EmailField = currentUxml.Q<TextField>("Email-Field");
                    PasswordField = currentUxml.Q<TextField>("Password-Field");
                    ErrorText = currentUxml.Q<Label>("ErrorOutput");
                    Button signInButton = currentUxml.Q<Button>("Authentication-Button");
                    signInButton.clicked += OnClickSignIn;
                    Button SignUpTab = currentUxml.Q<Button>("SignUp");
                    SignUpTab.clicked += LoadSignUpTab;
                    break;
            }
        }

        private void OnEnable()
        {
            UIManager.UxmlChanged += OnUxmlChange;
        }

        private void OnDisable()
        {
            UIManager.UxmlChanged -= OnUxmlChange;
        }
    }
}