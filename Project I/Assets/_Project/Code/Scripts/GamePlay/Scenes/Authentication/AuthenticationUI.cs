using UnityEngine;
using UnityEngine.UIElements;

namespace Wonderland.GamePlay.Authentication
{
    public class AuthenticationUI : UIHandler
    {
        public static AuthenticationUI Instance;
        
        [Header("UI Assets")]
        public VisualTreeAsset signUpUxml;
        public VisualTreeAsset signInUxml;

        #region Authentication Components
        
        public TextField EmailField;
        public TextField PasswordField;
        public TextField ConfirmPasswordField;
        public Label ErrorText;

        #endregion

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
            VisualElement currentUxml = MainManager.Instance.uiManager.currentUxml;
            switch (MainManager.Instance.uiManager.GetCurrentUxmlName())
            {
                case "SignUpPanel":
                    EmailField = currentUxml.Q<TextField>("Email-Field");
                    PasswordField = currentUxml.Q<TextField>("Password-Field");
                    ConfirmPasswordField = currentUxml.Q<TextField>("Confirm-Field");
                    ErrorText = currentUxml.Q<Label>("ErrorOutput");
                    var signUpButton = currentUxml.Q<Button>("Authentication-Button");
                    signUpButton.clicked += AuthenticationHandler.Instance.SignUp;
                    var SignInTab = currentUxml.Q<Button>("SignIn");
                    SignInTab.clicked += LoadSignInTab;
                    break;
                case "SignInPanel":
                    EmailField = currentUxml.Q<TextField>("Email-Field");
                    PasswordField = currentUxml.Q<TextField>("Password-Field");
                    ErrorText = currentUxml.Q<Label>("ErrorOutput");
                    var signInButton = currentUxml.Q<Button>("Authentication-Button");
                    signInButton.clicked += AuthenticationHandler.Instance.SignIn;
                    var SignUpTab = currentUxml.Q<Button>("SignUp");
                    SignUpTab.clicked += LoadSignUpTab;
                    break;
            }
        }

        private void Awake()
        {
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
            UIManager.UxmlChanged += OnUxmlChange;
        }

        private void OnDisable()
        {
            UIManager.UxmlChanged -= OnUxmlChange;
        }
    }
}
