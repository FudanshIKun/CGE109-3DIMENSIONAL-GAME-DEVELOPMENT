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

        public Dictionary<string,object> Info { get; set; }
        public Dictionary<string, object> GameData{ get; set; }

        #endregion

        public User(FirebaseUser user)
        {
            FirebaseUser = user;
            UserEmail = user.Email;
            GameData = new Dictionary<string, object>()
            {
                {"Last Checkpoint ID", 0},
                {"Energy Level", 0},
                //{"Inventory",  }
            };
            Info = new Dictionary<string, object>
            {
                {"UserName", null},
                {"DisplayName", user.DisplayName},
                {"GameData", GameData}
            };
        }
    }
}