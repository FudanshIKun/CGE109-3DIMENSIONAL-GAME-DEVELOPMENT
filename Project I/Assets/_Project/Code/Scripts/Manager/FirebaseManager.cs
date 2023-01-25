using System;
using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;

namespace Wonderland.Manager
{
    public class FirebaseManager : MonoBehaviour
    {
        #region Singleton
        
        public static FirebaseManager Instance;
        private void Singleton()
        {
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

        private FirebaseAuth _firebaseAuth;
        private FirebaseUser user;
        private DatabaseReference _databaseReference;
        [HideInInspector] public bool isAuthenticated;

        #region Authentication Methods

        /// <summary>
        /// This method is used to sign Up user to the game's database with email and password
        /// </summary>
        public void SignUp()
        {
            StartCoroutine(SignUpAsync(UIManager.Instance.userField.value, 
                UIManager.Instance.emailField.value, 
                UIManager.Instance.passwordField.value, 
                UIManager.Instance.confirmField.value));
        } 
        
        /// <summary>
        /// This method is used to sign In user to the game's database with email and password
        /// </summary>
        public void SignIn()
        {
            StartCoroutine(SignInAsync(UIManager.Instance.emailField.value, 
                UIManager.Instance.passwordField.value));
        } 
        
        private IEnumerator SignUpAsync(string _username ,string _email, string _password, string _confirmPassword)
        {
            if (_username == "")
            {
                UIManager.Instance.errorOutput.text = "Please Enter A Username";
            }
            else if (_email == "")
            {
                UIManager.Instance.errorOutput.text = "Please Enter A Email";
            }
            else if (_password != _confirmPassword)
            {
                UIManager.Instance.errorOutput.text = "Password Do Not Match!";
            }
            else
            {
                var signUpTask = _firebaseAuth.CreateUserWithEmailAndPasswordAsync(_email, _password);

                yield return new WaitUntil(predicate: () => signUpTask.IsCompleted);
                
                if (signUpTask.Exception != null)
                {
                    FirebaseException firebaseException = (FirebaseException)signUpTask.Exception.GetBaseException();
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

                    UIManager.Instance.errorOutput.text = output;
                }
                else
                {
                    //Get The User After Registration Success
                    user = signUpTask.Result;
                    UserProfile profile = new UserProfile
                    {
                        DisplayName = _username,

                        //TODO: Give Profile Default Photo
                        //PhotoUrl = new System.Uri("Default Photo Url!"),
                    };

                    var defaultUserTask = user.UpdateUserProfileAsync(profile);

                    yield return new WaitUntil(predicate: () => defaultUserTask.IsCompleted);
                    
                    if (defaultUserTask.Exception != null)
                    {
                        //Delete the user if user update failed
                        user.DeleteAsync();
                        FirebaseException firebaseException = (FirebaseException)defaultUserTask.Exception.GetBaseException();
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

                        UIManager.Instance.errorOutput.text = output;
                    }
                    else
                    {
                        Debug.Log($"Firebase User Created Successfully : {user.DisplayName} ({user.UserId})");

                        //Temporary
                        UIManager.Instance.ChangeUxml(UIManager.Instance.SignInUXML);
                        UIManager.Instance.errorOutput.text = "Please SignIn to Enter The Game";
                    }
                }
            }
        }
        
        private IEnumerator SignInAsync(string _email, string _password)
        {
            Credential credential = EmailAuthProvider.GetCredential(_email, _password);

            var signInTask = _firebaseAuth.SignInWithCredentialAsync(credential);

            yield return new WaitUntil(predicate: () => signInTask.IsCompleted);

            if (signInTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)signInTask.Exception.GetBaseException();
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

                UIManager.Instance.errorOutput.text = output;
            }
            else
            {
                user = signInTask.Result;
                isAuthenticated = true;
                Debug.LogFormat($"Successfully Signed In: {user.DisplayName}");
                GameManager.Instance.LoadScene(GameManager.SceneType.Lobby, ScreenOrientation.AutoRotation);
            }
        }
        
        public void UpdateProfilePicture(string newProfilePictureURL)
        {
            
        }

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
                    Debug.Log("Profile Image Updated Successfully");
                }
            }
        }

        public void SignOut()
        {
            _firebaseAuth.SignOut();
        }

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
                    Debug.Log("Signed Out");
                }
                
                user = _firebaseAuth.CurrentUser;
                if (signedIn)
                {
                    Debug.Log($"Signed In : {user.DisplayName} {user.UserId}");
                }
            }
        }

        #endregion
        
        private void Awake()
        {
            Singleton();
            UIManager.Instance.ChangeUxml(UIManager.Instance.SignInUXML);
            StartCoroutine(CheckAndFixDependencies());
        }

        void Start()
        {
            
        }

        private void Update()
        {
            if (user != null)
            {
                Debug.LogFormat($"User : {user.DisplayName} {user.UserId}");
            }
        }

        private void OnDestroy()
        {
            _firebaseAuth.StateChanged -= AuthStateChanged;
            _firebaseAuth = null;
        }

        private void OnApplicationQuit()
        {
            SignOut();
        }
    }
}
