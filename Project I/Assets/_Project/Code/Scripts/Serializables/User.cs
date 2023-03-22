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

        private string UserEmailRef { get; }
        public string UserEmail => UserEmailRef;

        private FirebaseUser FirebaseUserRef { get; }
        public FirebaseUser FirebaseUser => FirebaseUserRef;

        #endregion

        #region Dictionary

        private Dictionary<string,object> UserInfoDict { get; set; }
        public Dictionary<string, object> UserInfo
        {
            get => UserInfoDict;
            set => UserInfoDict = value;
        }
        
        private Dictionary<string, object> UserGameDataDict { get; set; }

        public Dictionary<string, object> UserGameData
        {
            get => UserGameDataDict;
            set => UserGameDataDict = value;
        }

        #endregion

        public User(FirebaseUser user)
        {
            FirebaseUserRef = user;
            UserEmailRef = user.Email;
            UserGameData = new Dictionary<string, object>()
            {
                
            };
            UserInfo = new Dictionary<string, object>
            {
                {"UserName", null},
                {"UserID", user.UserId},
                {"DisplayName", user.DisplayName},
                {"GameData", UserGameData}
            };
        }
    }
}
