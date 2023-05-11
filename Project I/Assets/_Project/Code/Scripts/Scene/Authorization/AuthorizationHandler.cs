using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Wonderland.Management;

namespace Wonderland.Scene.Authorization
{
    public class AuthorizationHandler : SerializedMonoBehaviour
    {
        #region Methods

        public static async void SignUp(string userName, string email, string password, string confirmPassword, Label error)
        {
            if(string.IsNullOrEmpty(userName))
            {
                error.text = "Please Enter your Email";
            }
            else if (string.IsNullOrEmpty(email))
            {
                error.text = "Please Enter your Email";
            }
            else if (password != confirmPassword)
            {
                error.text = "Please Check Your Password Again";
            }
            else
            {
                try
                {
                    GameManager.ChangeGameState(GameManager.State.LoadState);
                    await FirebaseManager.SignUpAsync(userName, email, password);
                    
                    CustomLog.Auth.Log("Sign Up completed");
                    SceneManager.LoadSceneAsync(sceneBuildIndex: 1);
                }
                catch (FirebaseException exception)
                {
                    exception = (FirebaseException)exception.GetBaseException();
                    
                    if (Enum.IsDefined(typeof(AuthError), exception.ErrorCode))
                    {
                        var authError = (AuthError)exception.ErrorCode;
                        error.text = AuthErrorHandling(authError);
                    }
                    else if (Enum.IsDefined(typeof(FirestoreError), exception.ErrorCode))
                    {
                        var firestoreError = (FirestoreError)exception.ErrorCode;
                        error.text = FirestoreErrorHandling(firestoreError);
                    }
                    else
                    {
                        error.text = "Unknown Error, Please Try Again";
                    }
                            
                    GameManager.ChangeGameState(GameManager.State.IdleState);
                }
            }
        }

        public static async void SignIn(string email, string password, Label error)
        {
            if (string.IsNullOrEmpty(email))
            {
                error.text = "Please Enter your Email";
            }
            else if (string.IsNullOrEmpty(password))
            {
                error.text = "Please Enter your Password";
            }
            else
            {
                GameManager.ChangeGameState(GameManager.State.LoadState); ;
                
                try
                {
                    await FirebaseManager.SignInAsync(email, password);
                    
                    CustomLog.Auth.Log("Sign In completed");
                    Management.SceneHandler.LoadSceneWithTransition(1);
                }
                catch (FirebaseException exception)
                {
                    exception = (FirebaseException)exception.GetBaseException();
                    CustomLog.Auth.Log("Base Exception: " + exception);
                        
                    if (Enum.IsDefined(typeof(AuthError), exception.ErrorCode))
                    {
                        var authError = (AuthError)exception.ErrorCode;
                        error.text = AuthErrorHandling(authError);
                    }
                    else if (Enum.IsDefined(typeof(FirestoreError), exception.ErrorCode))
                    {
                        var firestoreError = (FirestoreError)exception.ErrorCode;
                        error.text = FirestoreErrorHandling(firestoreError);
                    }
                    else
                    {
                        error.text = "Unknown Error, Please Try Again";
                    }
                    
                    GameManager.ChangeGameState(GameManager.State.IdleState);
                }
            }
        }

        public static async void ContinueAsGuest()
        {
            
        }
        
        private static string AuthErrorHandling(AuthError error)
        {
            var output = error switch
            {
                AuthError.InvalidEmail => "Invalid Email",
                AuthError.EmailAlreadyInUse => "Email Already In Use",
                AuthError.WeakPassword => "Weak Password",
                _ => "Unknown Error, Please Try Again"
            };

            return output;
        }

        private static string FirestoreErrorHandling(FirestoreError error)
        {
            var output = error switch
            {
                FirestoreError.PermissionDenied => "Permission To Database Has Denied",
                _ => "Unknown Error, Please Try Again"
            };
            return output;
        }

        #endregion
    }
}
