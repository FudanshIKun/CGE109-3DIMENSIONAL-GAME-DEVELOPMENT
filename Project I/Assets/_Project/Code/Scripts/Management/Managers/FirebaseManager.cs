using System;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine.UIElements;
using Wonderland.API;

namespace Wonderland.Management
{
    public class FirebaseManager : IManager
    {
        #region Fields

        private static FirebaseAuth Auth { get; set; }
        private static User CurrentUser { get; set; }
        
        private static FirebaseFirestore Firestore { get; set; }

        private static DocumentReference UserInfoDocumentReference{ get; set; }

        #endregion

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
                    GameManager.ChangeGameState(State.LoadState);
                    await AuthAPI.SignUp(Auth, email, password);
                    
                    CurrentUser = new User(Auth.CurrentUser)
                    {
                        UserName = userName,
                        UserInfo =
                        {
                            ["UserName"] = userName
                        } 
                    };
                    
                    UserInfoDocumentReference =
                        Firestore.Collection("Users").Document(Auth.CurrentUser.Email);
                    
                    await FirestoreAPI.PostToFirestore(UserInfoDocumentReference, CurrentUser.UserInfo);
                    
                    MainManager.Instance.GameManager.LoadSceneWithLoaderAsync
                        (Scene.BeatRunner);
                }
                catch (FirebaseException firebaseException)
                {
                    GameManager.ChangeGameState(State.IdleState);
                    FirebaseException exception = (FirebaseException)firebaseException.GetBaseException();
                    if (Enum.IsDefined(typeof(AuthError), exception.ErrorCode))
                    {
                        AuthError authError = (AuthError)exception.ErrorCode;
                        errorText.text = AuthErrorHandling(authError);
                    }
                    else if (Enum.IsDefined(typeof(FirestoreError), exception.ErrorCode))
                    {
                        FirestoreError firestoreError = (FirestoreError)exception.ErrorCode;
                        errorText.text = FirestoreErrorHandling(firestoreError);
                    }
                    else
                    {
                        errorText.text = "Unknown Error, Please Try Again";
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
                GameManager.ChangeGameState(State.LoadState);
                try
                {
                    await AuthAPI.SignIn(Auth, email, password);

                    CurrentUser = new User(Auth.CurrentUser);
                    
                    UserInfoDocumentReference =
                        Firestore.Collection("Users").Document(Auth.CurrentUser.Email);
                    
                    await FirestoreAPI.RetrieveFromFirestore(UserInfoDocumentReference).ContinueWith(loadUserInfoTask =>
                    {
                        CurrentUser.UserInfo = loadUserInfoTask.Result;
                    });
                    
                    MainManager.Instance.GameManager.LoadSceneWithLoaderAsync
                        (Scene.BeatRunner);
                }
                catch (FirebaseException firebaseException)
                {
                    FirebaseException exception = (FirebaseException)firebaseException.GetBaseException();
                    if (Enum.IsDefined(typeof(AuthError), exception.ErrorCode))
                    {
                        AuthError authError = (AuthError)exception.ErrorCode;
                        errorText.text = AuthErrorHandling(authError);
                    }
                    else if (Enum.IsDefined(typeof(FirestoreError), exception.ErrorCode))
                    {
                        FirestoreError firestoreError = (FirestoreError)exception.ErrorCode;
                        errorText.text = FirestoreErrorHandling(firestoreError);
                    }
                    else
                    {
                        errorText.text = "Unknown Error, Please Try Again";
                    }
                }
            }
        }

        public static void SignOutAsync()
        {
            AuthAPI.SignOut(Auth);
        }

        public static bool IsSignedIn() => Auth.CurrentUser != null;
        public static User GetCurrentUser() => CurrentUser;
        
        #endregion
        
        #region Initialize Firebase Methods

        private void InitializeFirebase()
        {
            Auth = FirebaseAuth.DefaultInstance;
            Firestore = FirebaseFirestore.DefaultInstance;
        }

        #endregion

        #region Error Handling

        private static string AuthErrorHandling(AuthError error)
        {
            string output = "Unknown Error, Please Try Again";
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
            }

            return output;
        }

        private static string FirestoreErrorHandling(FirestoreError error)
        {
            string output = "Unknown Error, Please Try Again";
            switch (error)
            {
                case FirestoreError.PermissionDenied:
                    output = "Permission To Database Has Denied";
                    break;
            }
            return output;
        }

        #endregion

        private void Awake()
        {
            InitializeFirebase();
        }

        private void OnApplicationQuit()
        {
            SignOutAsync();
        }
    }
}
