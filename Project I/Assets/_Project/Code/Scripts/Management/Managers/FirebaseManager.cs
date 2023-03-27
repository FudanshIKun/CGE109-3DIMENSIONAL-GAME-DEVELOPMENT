using System;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.UIElements;
using Wonderland.API;

namespace Wonderland.Management
{
    public class FirebaseManager : IManager
    {
        
        
        #region Methods
        
        public static async void SignUpAsync(string userName, string email, string password, string confirmPassword, Label errorText)
        {
            if(string.IsNullOrEmpty(userName))
            {
                errorText.text = "Please Enter your Email";
            }
            else if (string.IsNullOrEmpty(email))
            {
                errorText.text = "Please Enter your Email";
            }
            else if (password != confirmPassword)
            {
                errorText.text = "Please Check Your Password Again";
            }
            else
            {
                try
                {
                    await AuthAPI.SignUp(userName, email, password);
                    MainManager.Instance.gameManager.LoadSceneWithLoaderAsync
                        (IManager.SceneType.Lobby);
                }
                catch (FirebaseException firebaseException)
                {
                    FirebaseException exception = (FirebaseException)firebaseException.GetBaseException();
                    string output = "Unknown Error, Please Try Again";
                    if (Enum.IsDefined(typeof(AuthError), exception.ErrorCode))
                    {
                        AuthError authError = (AuthError)exception.ErrorCode;
                        switch (authError)
                        {
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
                    else if (Enum.IsDefined(typeof(FirestoreError), exception.ErrorCode))
                    {
                        FirestoreError firestoreError = (FirestoreError)exception.ErrorCode;
                        // handle Firestore error
                        errorText.text = output;
                    }
                    else
                    {
                        errorText.text = output;
                    }
                }
            }
        }

        public static async void SignInAsync(string email, string password, Label errorText)
        {
            if(string.IsNullOrEmpty(email))
            {
                errorText.text = "Please Enter your Email";
            }
            else if (string.IsNullOrEmpty(password))
            {
                errorText.text = "Please Enter your Password";
            }
            else
            {
                try
                {
                    await AuthAPI.SignIn(email, password);
                    MainManager.Instance.gameManager.LoadSceneWithLoaderAsync
                        (SceneType.Lobby);
                }
                catch (FirebaseException firebaseException)
                {
                    FirebaseException exception = (FirebaseException)firebaseException.GetBaseException();
                    string output = "Unknown Error, Please Try Again";
                    if (Enum.IsDefined(typeof(AuthError), exception.ErrorCode))
                    {
                        AuthError authError = (AuthError)exception.ErrorCode;
                        switch (authError)
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
                        }
                        errorText.text = output;
                    }
                    else if (Enum.IsDefined(typeof(FirestoreError), exception.ErrorCode))
                    {
                        FirestoreError firestoreError = (FirestoreError)exception.ErrorCode;
                        // handle Firestore error
                        errorText.text = output;
                    }
                    else
                    {
                        errorText.text = output;
                    }
                }
            }
        }

        public static async void SignOutAsync()
        {
            
        }
        
        #endregion
    }
}
