using System;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Firestore;

namespace Wonderland
{
    [Serializable][FirestoreData]
    public class User
    {
        #region Fields

        public FirebaseUser FirebaseUser { get; private set; }
        public string UserEmail { get; private set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }

        #endregion

        #region Dictionary

        public Dictionary<string,object> UserInfo { get; set; }

        public Dictionary<string, object> UserGameData{ get; set; }

        #endregion

        public User(FirebaseUser user)
        {
            FirebaseUser = user;
            UserName = null;
            UserEmail = user.Email;
            UserGameData = new Dictionary<string, object>()
            {
                
            };
            UserInfo = new Dictionary<string, object>
            {
                {"UserID", user.UserId},
                {"UserName", null},
                {"DisplayName", user.DisplayName},
                {"GameData", UserGameData}
            };
        }
    }
}
