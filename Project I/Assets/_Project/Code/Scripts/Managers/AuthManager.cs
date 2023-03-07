using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

// APIs
using Firebase;
using Firebase.Auth;


namespace Wonderland
{
    public class AuthManager : IManager
    {
        private string AuthKey = "";
        private string DatabaseURL = "";

        public static async Task SignUpAsync(string email, string password, string confirmPassword,
            Label errorText)
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
                await AuthAPI.SignUpRequest(email, password).ContinueWith(task =>
                {
                    if (task.Exception != null)
                    {
                        FirebaseException firebaseException = (FirebaseException)task.Exception.GetBaseException();
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
                        //TODO: Post Newuser To Database After SignUp Successfully
                        //Debug.LogFormat($"Firebase User Created Successfully : {_user.DisplayName} ({_user.UserId})");
                    }
                });
            }
        }

        public static async Task SignInAsync(string email, string password, Label errorText)
        {
            await AuthAPI.SignInRequest(email, password).ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    FirebaseException firebaseException = (FirebaseException)task.Exception.GetBaseException();
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
                    AuthAPI.IsSignedIn();
                    //Debug.LogFormat($"Successfully Signed In: {_user.DisplayName} {_user.UserId}");
                }
            });
        }

        private void Awake()
        {
            
        }

        private void OnEnable()
        {
            
        }

        private void OnApplicationQuit()
        {
            AuthAPI.SignOut();
        }
    }
}
