using System;
using Firebase.Auth;
using UnityEngine;

namespace Wonderland
{
    [Serializable]
    public class User : MonoBehaviour
    {
        public FirebaseUser authUser;
        public string displayName;
        public string userId;

        public User(FirebaseUser user)
        {
            authUser = user;
            displayName = authUser.DisplayName;
            userId = authUser.UserId;
        }
    }
}
