using System.Collections;
using UnityEngine;
using Wonderland.Utility;

//Firebase
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Storage;

//Project
using Wonderland.Manager;

namespace Wonderland.Auth
{
    public class AuthManager : MonoBehaviour
    {
        private FirebaseUser _user;
        private FirebaseAuth _auth;
        private DatabaseReference _databaseReference;
        private FirebaseStorage _storage;
        private StorageReference _storageReferencee;
        
        [HideInInspector] public bool isAuthenticated;

        #region Singleton
        
        public static AuthManager Instance;
        private void Singleton()
        {
            DontDestroyOnLoad(gameObject);
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(Instance.gameObject);
                Instance = this;
            }
        }

        #endregion
        
        #region Authentication Methods
        
        public void SignUp()
        {
            StartCoroutine(SignUpAsync(
                AuthUI.Instance.UserField.value, 
                AuthUI.Instance.EmailField.value, 
                AuthUI.Instance.PasswordField.value,
                AuthUI.Instance.ConfirmField.value));
        } 
        
        public void SignIn()
        {
            StartCoroutine(SignInAsync(
                AuthUI.Instance.EmailField.value, 
                AuthUI.Instance.PasswordField.value));
        } 
        
        private IEnumerator SignUpAsync(string username, string email, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(username))
            {
                AuthUI.Instance.ErrorOutput.text = "Please Enter A Username";
            }
            else if (string.IsNullOrEmpty(email))
            {
                AuthUI.Instance.ErrorOutput.text = "Please Enter A Email";
            }
            else if (password != confirmPassword)
            {
                AuthUI.Instance.ErrorOutput.text = "Password Do Not Match!";
            }
            else
            {
                var signUpTask = _auth.CreateUserWithEmailAndPasswordAsync(email, password);

                yield return new WaitUntil(predicate: () => signUpTask.IsCompleted);
                
                if (signUpTask.Exception != null)
                {
                    FirebaseException firebaseException = 
                        (FirebaseException)signUpTask.Exception.GetBaseException();
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

                    AuthUI.Instance.ErrorOutput.text = output;
                }
                else
                {
                    //Get The User After Registration Success
                    FirebaseUser newUser = signUpTask.Result;
                    
                    //Create User Authentication Profile
                    UserProfile profile = new UserProfile
                    {
                        DisplayName = username,

                        //TODO: Give Profile Default Photo
                        //PhotoUrl = new System.Uri("Default Photo Url!"),
                    };

                    var defaultUserTask = newUser.UpdateUserProfileAsync(profile);

                    yield return new WaitUntil(predicate: () => defaultUserTask.IsCompleted);
                    
                    if (defaultUserTask.Exception != null)
                    {
                        //Delete the user if user update failed
                        newUser.DeleteAsync();
                        FirebaseException firebaseException = 
                            (FirebaseException)defaultUserTask.Exception.GetBaseException();
                        AuthError error = (AuthError)firebaseException.ErrorCode;
                        string output = "Unknow Error, Please Try Again";

                        switch (error)
                        {
                            case AuthError.Cancelled:
                                output = "Update User Cancled";
                                break;
                            case AuthError.SessionExpired:
                                output = "Session Expired";
                                break;
                        }

                        AuthUI.Instance.ErrorOutput.text = output;
                    }
                    else
                    {
                        _user = newUser;
                        Debug.LogFormat($"Firebase User Created Successfully : {_user.DisplayName} ({_user.UserId})");

                        var databaseUserInfoTask = _databaseReference.Child("users").Child(_user.UserId).Child("username").SetValueAsync(username);

                        yield return new WaitUntil(predicate: (() => databaseUserInfoTask.IsCompleted));

                        if (databaseUserInfoTask.Exception != null)
                        {
                            FirebaseException firebaseException = 
                                (FirebaseException)databaseUserInfoTask.Exception.GetBaseException();
                            AuthError error = (AuthError)firebaseException.ErrorCode;
                            string output = "Unknow Error, Please Try Again";

                            switch (error)
                            {
                                case AuthError.Failure:
                                    output = "Database Updating Failed";
                                    break;
                                case AuthError.SessionExpired:
                                    output = "Session Expired ,Please Try Again";
                                    break;
                            }

                            AuthUI.Instance.ErrorOutput.text = output;
                        }
                        else
                        {
                            #region Default GameData For New User

                            _databaseReference.Child("users").Child(_user.UserId).Child("GameData")
                                .Child("Cat In Posession").SetValueAsync(0);
                            _databaseReference.Child("users").Child(_user.UserId).Child("GameData").Child("");

                            #endregion
                            
                            UIManager.Instance.ChangeUxml(AuthUI.Instance.signInUxml);
                            AuthUI.Instance.ErrorOutput.text = "Please SignIn to Enter The Game";
                        }
                    }
                }
            }
        }
        
        private IEnumerator SignInAsync(string email, string password)
        {
            Credential credential = EmailAuthProvider.GetCredential(email, password);

            var signInTask = _auth.SignInWithCredentialAsync(credential);

            yield return new WaitUntil(predicate: () => signInTask.IsCompleted);

            if (signInTask.Exception != null)
            {
                FirebaseException firebaseException = 
                    (FirebaseException)signInTask.Exception.GetBaseException();
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

                AuthUI.Instance.ErrorOutput.text = output;
            }
            else
            {
                _user = signInTask.Result;
                
                //TODO: Check If User SignIn For The First Time With Timestamp
                Logging.FirebaseLogger.Log(_user.Metadata.CreationTimestamp);

                isAuthenticated = true;
                Debug.LogFormat($"Successfully Signed In: {_user.DisplayName} {_user.UserId}");
                GameManager.Instance.LoadSceneWithLoaderAsync
                    (GameManager.SceneType.Lobby);
            }
        }
        
        public void SignOut()
        {
            StartCoroutine(SignOutAsync());
        }
        
        private IEnumerator SignOutAsync()
        {
            if (_auth != null && _user != null)
            {
                Debug.LogFormat($"Signed Out: {_user.DisplayName} {_user.UserId}");
                _auth.SignOut();
            }
            yield return null;
        }
        

        #endregion
        
        #region Initialization

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator CheckAndFixDependencies()
        {
            var checkAndFixDependencies = FirebaseApp.CheckAndFixDependenciesAsync();

            yield return new WaitUntil(predicate: () => checkAndFixDependencies.IsCompleted);

            var dependencyResult = checkAndFixDependencies.Result;
            if (dependencyResult == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyResult}");
            }
        }
        
        private void InitializeFirebase()
        {
            _auth = FirebaseAuth.DefaultInstance;
            _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            _storage = FirebaseStorage.DefaultInstance;
            _storageReferencee = FirebaseStorage.DefaultInstance.RootReference;

            _auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
        }
        
        private void AuthStateChanged(object sender, System.EventArgs eventArgs)
        {
            //checks if the local user is the same as the one from the auth
            if (_auth.CurrentUser != _user)
            {
                bool signedIn = _user != _auth.CurrentUser && _auth.CurrentUser != null;
                if (!signedIn && _user != null)
                {
                    Logging.FirebaseLogger.Log("Signed Out");
                }
                
                _user = _auth.CurrentUser;
                if (signedIn)
                {
                    Logging.FirebaseLogger.Log($"Signed In : {_user.DisplayName} {_user.UserId}");
                    Debug.Log($"Signed In : {_user.DisplayName} {_user.UserId}");
                }
            }
        }

        #endregion
        
        private void Awake()
        {
            Singleton();
            StartCoroutine(CheckAndFixDependencies());
        }
        
        private void OnApplicationQuit()
        {
            SignOut();
        }
        
        private void OnDestroy()
        {
            _auth.StateChanged -= AuthStateChanged;
            _auth = null;
        }
    }
}
