using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Wonderland.Manager;
namespace Wonderland.Auth
{
    public class AuthUI : MonoBehaviour
    {
        public static AuthUI Instance;
        
        [Header("UI Assets")]
        public VisualTreeAsset signUpUxml;
        public VisualTreeAsset signInUxml;

        #region Authentication Components

        public TextField UserField;
        public TextField EmailField;
        public TextField PasswordField;
        public TextField ConfirmField;
        public RadioButton RememberPassword;
        public Label ErrorOutput;

        #endregion

        private void LoadSignUpTab()
        {
            UIManager.Instance.ChangeUxml(signUpUxml);
        }
        
        private void LoadSignInTab()
        {
            UIManager.Instance.ChangeUxml(signInUxml);
        }
        private void OnUxmlChange()
        {
            VisualElement currentUxml = UIManager.Instance.currentUxml;
            switch (UIManager.Instance.GetCurrentUxmlName())
            {
                case "SignUpPanel":
                    UserField = currentUxml.Q<TextField>("User-Field");
                    EmailField = currentUxml.Q<TextField>("Email-Field");
                    PasswordField = currentUxml.Q<TextField>("Password-Field");
                    ConfirmField = currentUxml.Q<TextField>("Confirm-Field");
                    RememberPassword = currentUxml.Q<RadioButton>("RememberPassowrd");
                    ErrorOutput = currentUxml.Q<Label>("ErrorOutput");
                    var signUpButton = currentUxml.Q<Button>("Authentication-Button");
                    signUpButton.clicked += AuthManager.Instance.SignUp;
                    var SignInTab = currentUxml.Q<Button>("SignIn");
                    SignInTab.clicked += LoadSignInTab;
                    break;
                case "SignInPanel":
                    EmailField = currentUxml.Q<TextField>("Email-Field");
                    PasswordField = currentUxml.Q<TextField>("Password-Field");
                    RememberPassword = currentUxml.Q<RadioButton>("RememberPassword");
                    ErrorOutput = currentUxml.Q<Label>("ErrorOutput");
                    var signInButton = currentUxml.Q<Button>("Authentication-Button");
                    signInButton.clicked += AuthManager.Instance.SignIn;
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
