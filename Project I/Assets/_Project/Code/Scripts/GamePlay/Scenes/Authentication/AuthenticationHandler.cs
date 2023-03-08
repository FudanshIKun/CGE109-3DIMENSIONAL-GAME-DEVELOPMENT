using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Firebase;
using Firebase.Auth;

namespace Wonderland.GamePlay.Authentication
{
    public class AuthenticationHandler : SceneHandler
    {
        public static AuthenticationHandler Instance;

        #region Methods

        public void OnClickSignUp() => StartCoroutine(SignUpCoroutine(AuthenticationUI.Instance.EmailField.value, AuthenticationUI.Instance.PasswordField.value, AuthenticationUI.Instance.ConfirmPasswordField.value, AuthenticationUI.Instance.ErrorText));

        public void OnClickSignIn() => StartCoroutine(SignInCoroutine(AuthenticationUI.Instance.EmailField.value, AuthenticationUI.Instance.PasswordField.value, AuthenticationUI.Instance.ErrorText));
        
        private IEnumerator SignUpCoroutine(string email, string password, string confirmPassword, Label errorText)
        {
            var signUpTask = AuthManager.SignUpAsync(email, password, confirmPassword, errorText);

            yield return new WaitUntil(predicate: () => signUpTask.IsCompleted);
            
            if(signUpTask.Exception == null)
            {
                MainManager.Instance.uiManager.ChangeUxml(AuthenticationUI.Instance.signInUxml);
            }
        }
        
        private IEnumerator SignInCoroutine(string email, string password, Label errorText)
        {
            var signInTask = AuthManager.SignInAsync(email, password, errorText);

            yield return new WaitUntil(predicate: () => signInTask.IsCompleted);
            
            if(signInTask.Exception == null)
            {
                MainManager.Instance.gameManager.LoadSceneWithLoaderAsync
                    (GameManager.SceneType.Lobby);
            }
        }

        #endregion

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

        private void Start()
        {
            MainManager.Instance.uiManager.ChangeUxml(AuthenticationUI.Instance.signInUxml);
        }
    }
}
