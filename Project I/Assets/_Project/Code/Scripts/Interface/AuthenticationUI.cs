using UnityEngine;
using UnityEngine.UIElements;
using Wonderland.Manager;

namespace Wonderland.Interface
{
    public class AuthenticationUI : MonoBehaviour
    {
        private VisualElement _root;
        private Button signUpButton;
        private Button signInButton;

        private void SignUpPressed()
        {
            UIManager.Instance.ChangeUxml(
                UIManager.Instance.SignUpUXML);
        }

        private void SignInPressed()
        {
            UIManager.Instance.ChangeUxml(
                UIManager.Instance.SignInUXML);
        }

        private void AddActions()
        {
            signUpButton.clicked += SignUpPressed;
            signInButton.clicked += SignInPressed;
        }

        private void Start()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            signUpButton = _root.Q<Button>("SignUp");
            signInButton = _root.Q<Button>("SignIn");
            AddActions();
        }
    }
}
