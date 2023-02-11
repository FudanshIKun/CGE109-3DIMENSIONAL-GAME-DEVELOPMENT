using System;
using System.Collections;
using UnityEngine;

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
    public class FirebaseManager : MonoBehaviour
    {
        #region Singleton
        
        public static FirebaseManager Instance;
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

        private FirebaseUser user;
        private FirebaseAuth _firebaseAuth;
        private DatabaseReference _databaseReference;
        private FirebaseStorage _storage;
        private StorageReference _storageReferencee;
        [HideInInspector] public bool isAuthenticated;

        #region Authentication Methods

        /// <summary>
        /// This method is used to sign Up user to the Authentication System with email and password
        /// </summary>
        public void SignUp()
        {
            StartCoroutine(SignUpAsync(AuthenticationUI.Instance.userField.value, 
                AuthenticationUI.Instance.emailField.value, 
                AuthenticationUI.Instance.passwordField.value, 
                AuthenticationUI.Instance.confirmField.value));
        } 
        
        /// <summary>
        /// This method is used to sign In user to the Authentication System with email and password
        /// </summary>
        public void SignIn()
        {
            StartCoroutine(SignInAsync(AuthenticationUI.Instance.emailField.value, 
                AuthenticationUI.Instance.passwordField.value));
        } 
        
        private IEnumerator SignUpAsync(string username ,string email, string password, string confirmPassword)
        {
            if (username == "")
            {
                AuthenticationUI.Instance.errorOutput.text = "Please Enter A Username";
            }
            else if (email == "")
            {
                AuthenticationUI.Instance.errorOutput.text = "Please Enter A Email";
            }
            else if (password != confirmPassword)
            {
                AuthenticationUI.Instance.errorOutput.text = "Password Do Not Match!";
            }
            else
            {
                var signUpTask = _firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password);

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

                    AuthenticationUI.Instance.errorOutput.text = output;
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

                        AuthenticationUI.Instance.errorOutput.text = output;
                    }
                    else
                    {
                        user = newUser;
                        Debug.LogFormat($"Firebase User Created Successfully : {user.DisplayName} ({user.UserId})");

                        var databaseUserInfoTask = _databaseReference.Child("users").Child(user.UserId).Child("username").SetValueAsync(username);

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

                            AuthenticationUI.Instance.errorOutput.text = output;
                        }
                        else
                        {
                            #region Default GameData For New User

                            _databaseReference.Child("users").Child(user.UserId).Child("GameData")
                                .Child("Cat In Posession").SetValueAsync(0);
                            _databaseReference.Child("users").Child(user.UserId).Child("GameData").Child("");

                            #endregion
                            
                            UIManager.Instance.ChangeUxml(AuthenticationUI.Instance.SignInUXML);
                            AuthenticationUI.Instance.errorOutput.text = "Please SignIn to Enter The Game";
                        }
                    }
                }
            }
        }
        
        private IEnumerator SignInAsync(string email, string password)
        {
            Credential credential = EmailAuthProvider.GetCredential(email, password);

            var signInTask = _firebaseAuth.SignInWithCredentialAsync(credential);

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

                AuthenticationUI.Instance.errorOutput.text = output;
            }
            else
            {
                user = signInTask.Result;
                
                //TODO: Check If User SignIn For The First Time With Timestamp
                Logging.FirebaseLogger.Log(user.Metadata.CreationTimestamp);

                isAuthenticated = true;
                Debug.LogFormat($"Successfully Signed In: {user.DisplayName} {user.UserId}");
                GameManager.Instance.LoadSceneWithLoaderAsync
                    (GameManager.SceneType.Lobby);
            }
        }
        
        /*
        private IEnumerator UpdateProfilePictureAsync(string newProfilePictureURL)
        {
            if (user != null)
            {
                UserProfile profile = new UserProfile();
                try
                {
                    UserProfile _profile = new UserProfile()
                    {
                        DisplayName = profile.DisplayName,
                        PhotoUrl = new System.Uri(newProfilePictureURL),
                    };
                    profile = _profile;
                }
                catch
                {
                    yield break;
                }

                var ProfilePictureTask = user.UpdateUserProfileAsync(profile);
                yield return new WaitUntil(predicate:() => ProfilePictureTask.IsCompleted);

                if (ProfilePictureTask.Exception != null)
                {
                    Debug.LogError($"Update Profile Picture was unseccessful: {ProfilePictureTask.Exception}");
                }
                else
                {
                    //TODO: Add LobbyManager "ChangeProfilePicture()"
                    //LobbyManager.Instance.ChangeProfilePicture();
                    Logging.FirebaseLogger.Log("Profile Image Updated Successfully");
                }
            }
        }
        */

        /// <summary>
        /// This method is used to sign Out user Out of the Authentication System
        /// </summary>
        public void SignOut()
        {
            if (_firebaseAuth != null && user != null)
            {
                Debug.LogFormat($"Signed Out: {user.DisplayName} {user.UserId}");
                _firebaseAuth.SignOut();
            }
        }

        /*
        private IEnumerator SignOutAsync()
        {
            if (_firebaseAuth != null && user != null)
            {
                Debug.LogFormat($"Signed Out: {user.DisplayName} {user.UserId}");
                _firebaseAuth.SignOut();
            }
            yield return null;
        }
        */

        #endregion

        #region Database Methods

        

        #endregion

        #region Firebase Initialization

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
        
        // Handle Initiailization of the necessary firebase modules
        private void InitializeFirebase()
        {
            _firebaseAuth = FirebaseAuth.DefaultInstance;
            _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            _storage = FirebaseStorage.DefaultInstance;
            _storageReferencee = FirebaseStorage.DefaultInstance.RootReference;

            _firebaseAuth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
        }
        
        // Track state changes of the auth object.
        private void AuthStateChanged(object sender, System.EventArgs eventArgs)
        {
            //checks if the local user is the same as the one from the auth
            if (_firebaseAuth.CurrentUser != user)
            {
                bool signedIn = user != _firebaseAuth.CurrentUser && _firebaseAuth.CurrentUser != null;
                if (!signedIn && user != null)
                {
                    Logging.FirebaseLogger.Log("Signed Out");
                }
                
                user = _firebaseAuth.CurrentUser;
                if (signedIn)
                {
                    Logging.FirebaseLogger.Log($"Signed In : {user.DisplayName} {user.UserId}");
                    Debug.Log($"Signed In : {user.DisplayName} {user.UserId}");
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
            _firebaseAuth.StateChanged -= AuthStateChanged;
            _firebaseAuth = null;
        }
    }
}
