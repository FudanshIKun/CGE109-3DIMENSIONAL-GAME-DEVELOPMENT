using System;
using UnityEngine;

namespace Wonderland
{
    [Serializable]
    public class User : MonoBehaviour
    {
        public string displayName;
        public string userId;

        public User()
        {
            displayName = AuthAPI.GetAuthUser().DisplayName;
            userId = AuthAPI.GetAuthUser().UserId;
        }
    }
}
