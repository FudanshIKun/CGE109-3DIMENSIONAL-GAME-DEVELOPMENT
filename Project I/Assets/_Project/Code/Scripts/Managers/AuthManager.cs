using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

// APIs
using Firebase;
using Firebase.Auth;


namespace Wonderland
{
    public class AuthManager : IManager
    {
        public FirebaseUser _user;

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
