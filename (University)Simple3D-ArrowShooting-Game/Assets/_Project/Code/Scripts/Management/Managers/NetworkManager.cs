using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Networking;
using Wonderland.API;

namespace Wonderland.Management
{
    public class NetworkManager : SerializedMonoBehaviour
    {
        public static NetworkManager Instance { get; private set; }
        
        [ShowInInspector]
        private static User CurrentUser
        {
            get; 
            set;
        }
        
        public static bool HasConnection { get; private set; }
        public static bool IsSignedIn() => Auth.CurrentUser != null;

        private static FirebaseAuth Auth { get; set; }
        private static FirebaseFirestore Firestore { get; set; }
        private static DocumentReference UserDocumentReference{ get; set; }
        

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

        private void OnApplicationQuit()
        {
            SignOut();
        }

        #region Methods

        public static IEnumerator TryConnection(Action<bool> action)
         {
             if (Application.internetReachability != NetworkReachability.NotReachable)
             {
                 var request = new UnityWebRequest("https://www.google.com/");
                 yield return request.SendWebRequest();
                 if (request.error != null)
                 {
                     CustomLog.Network.Log("TryConnection Failed");
                     HasConnection = false;
                     action(false);
                 }
                 else
                 {
                     CustomLog.Network.Log("TryConnection Success");
                     HasConnection = true;
                     action(true);
                     InitializeFirebase();
                 }
             }
             else
             {
                 CustomLog.Network.Log("Device Does Not Have Internet Reachbility");
                 action(false);
             }
         }

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

        public static User GetCurrentUser() => CurrentUser;
        
        #endregion
        
        #region Initialize Firebase Methods

        private static async void InitializeFirebase()
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