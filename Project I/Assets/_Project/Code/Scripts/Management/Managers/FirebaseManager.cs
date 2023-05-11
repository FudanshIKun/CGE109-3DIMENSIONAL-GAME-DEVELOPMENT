using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Sirenix.OdinInspector;
using UnityEngine;
using Wonderland.API;

namespace Wonderland.Management
{
    public class FirebaseManager : SerializedMonoBehaviour
    {
        public static FirebaseManager Instance { get; private set; }
        
        [ShowInInspector]
        private static User CurrentUser
        {
            get; 
            set;
        }
        
        public static bool IsSignedIn() => Auth.CurrentUser != null;

        #region Fields

        private static FirebaseAuth Auth { get; set; }
        private static FirebaseFirestore Firestore { get; set; }
        private static DocumentReference UserDocumentReference{ get; set; }

        #endregion

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            InitializeFirebase();
        }

        private void OnApplicationQuit()
        {
            SignOut();
        }

        #region Methods
        
        public static User GetCurrentUser() => CurrentUser;
        
        public static async Task<FirebaseException> SignUpAsync(string userName, string email, string password)
        {
            FirebaseException firebaseException = null;
            
            try
            {
                CustomLog.Auth.Log("Signing Up...");
                await AuthAPI.SignUp(Auth, email, password);
                    
                CurrentUser = new User(Auth.CurrentUser)
                {
                    UserName = userName,
                    Info =
                    {
                        ["UserName"] = userName
                    }
                };
                    
                UserDocumentReference = Firestore.Collection("Users").Document(Auth.CurrentUser.Email);
                    
                await FirestoreAPI.Post(UserDocumentReference, CurrentUser.Info);
            }
            catch (FirebaseException exception)
            {
                firebaseException = exception;
            }

            return firebaseException;
        }

        public static async Task<FirebaseException> SignInAsync(string email, string password)
        {
            FirebaseException firebaseException = null;
            
            try
            {
                CustomLog.Auth.Log("Signing In...");
                await AuthAPI.SignIn(Auth, email, password);

                CurrentUser = new User(Auth.CurrentUser);
                    
                UserDocumentReference = Firestore.Collection("Users").Document(Auth.CurrentUser.Email);

                CustomLog.Auth.Log("Retrieving User Info...");
                await FirestoreAPI.Retrieve(UserDocumentReference).ContinueWith(loadUserInfoTask =>
                {
                    CurrentUser.Info = loadUserInfoTask.Result;
                });

                CurrentUser.UserName = CurrentUser.Info["UserName"].ToString();
                CurrentUser.DisplayName = CurrentUser.Info["DisplayName"].ToString();
                
                CustomLog.Auth.Log("Sign In User Success!");
            }
            catch (FirebaseException exception)
            {
                firebaseException = exception;
                
            }

            return firebaseException;
        }

        public static void SignOut()
        {
            AuthAPI.SignOut(Auth);
        }

        #endregion
        
        #region Initialize Firebase Methods

        private void InitializeFirebase()
        {
            try
            {
                Auth = FirebaseAuth.DefaultInstance;
                Firestore = FirebaseFirestore.DefaultInstance;
            }
            catch (InitializationException e)
            {
                CustomLog.Auth.Log(e);
                throw;
            }
        }
        
        #endregion
    }
}