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

        public void SignUp() => StartCoroutine(SignUpCoroutine(AuthenticationUI.Instance.EmailField.value, AuthenticationUI.Instance.PasswordField.value, AuthenticationUI.Instance.ConfirmPasswordField.value, AuthenticationUI.Instance.ErrorText));

        public void SignIn() => StartCoroutine(SignInCoroutine(AuthenticationUI.Instance.EmailField.value, AuthenticationUI.Instance.PasswordField.value, AuthenticationUI.Instance.ErrorText));
        
        private IEnumerator SignUpCoroutine(string email, string password, string confirmPassword, Label errorText)
        {
            if (string.IsNullOrEmpty(email))
            {
                errorText.text = "Please Enter your Email";
            }
            else if (password != confirmPassword)
            {
                errorText.text = "Please Check Your Password Again";
            }
            else
            {
                var authTask = AuthAPI.SignUp(email, password);

                yield return new WaitUntil(predicate: () => authTask.IsCompleted);

                if (authTask.Exception != null)
                {
                    FirebaseException firebaseException =
                        (FirebaseException)authTask.Exception.GetBaseException();
                    AuthError error = (AuthError)firebaseException.ErrorCode;
                    string output = "Unknow Error, Please Try Again";

                    switch (error)
                    {
                        case AuthError.InvalidEmail:
                            output = "Invalid Email";
                            break;
                        case AuthError.EmailAlreadyInUse:
                            output = "Email Already In Use";
                            break;
                        case AuthError.WeakPassword:
                            output = "Weak Password";
                            break;
                        case AuthError.MissingEmail:
                            output = "Please Enter Your Email";
                            break;
                        case AuthError.MissingPassword:
                            output = "Please Enter Your Password";
                            break;
                    }

                    errorText.text = output;
                }
                else
                {
                    //TODO: User Management After SignUp Successfully
                    //Debug.LogFormat($"Firebase User Created Successfully : {_user.DisplayName} ({_user.UserId})");
                    MainManager.Instance.uiManager.ChangeUxml(AuthenticationUI.Instance.signInUxml);
                }
            }
        }
        
        private IEnumerator SignInCoroutine(string email, string password, Label errorText)
        {
            var authTask = AuthAPI.SignIn(email, password);

            yield return new WaitUntil(predicate: () => authTask.IsCompleted);

            if (authTask.Exception != null)
            {
                FirebaseException firebaseException = 
                    (FirebaseException)authTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;
                string output = "Unknow Error, Please Try Again";

                switch (error)
                {
                    case AuthError.MissingEmail:
                        output = "Please Enter Your Email";
                        break;
                    case AuthError.MissingPassword:
                        output = "Please Enter Your Password";
                        break;
                    case AuthError.InvalidEmail:
                        output = "Invalid Email";
                        break;
                    case AuthError.WrongPassword:
                        output = "Incorrect Password";
                        break;
                    case AuthError.UserNotFound:
                        output = "Account Does Not Exist";
                        break;
                }

                errorText.text = output;
            }
            else
            {
                //TODO: User Management After SignIn Successfully
                //Debug.LogFormat($"Successfully Signed In: {_user.DisplayName} {_user.UserId}");
                MainManager.Instance.gameManager.LoadSceneWithLoaderAsync
                    (GameManager.SceneType.Lobby);
            }
        }

        #endregion
    }
}
