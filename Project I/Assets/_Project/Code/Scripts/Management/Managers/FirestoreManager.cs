using System.Threading.Tasks;
using UnityEngine;
using Wonderland.API;

namespace Wonderland.Management
{
    public class FirestoreManager : IManager
    {
        public static async Task PostUserInfoToFireStore(User user) =>
            await FirestoreAPI.PostToFirestoreRequest(FirestoreAPI.UserInfoDocRef, user.UserInfo).ContinueWith(
                postUserInfoTask =>
                {
                    if (postUserInfoTask.Exception != null)
                    {
                        Logging.FirestoreLogger.Log("Post User Information To Firestore Encounter Exception: " + postUserInfoTask.Exception);
                    }
                    else
                    {
                        Debug.LogFormat(
                            $"Successfully Post User: {AuthManager.CurrentUser.UserInfo["UserName"]}'s Information To Firestore");
                    }
                });


        public static async Task RepostUserInfoToFireStore(User user)
        {
            await FirestoreAPI.RepostToFirestoreRequest(FirestoreAPI.UserInfoDocRef, user.UserInfo);
            Logging.FirestoreLogger.Log("Repost UserInfo Successfully");
        }

        // public static async Task UpdateUserInfoToFirestore(string[] targetFields, User user)
        // {
        //     
        // }

        public static async Task LoadUserInfoFromFirestore() => 
            await FirestoreAPI.RetrieveFromFirestoreRequest(FirestoreAPI.UserInfoDocRef).ContinueWith(loadUserInfoTask =>
            {
                if (loadUserInfoTask.Exception == null)
                {
                    AuthManager.CurrentUser.UserInfo = loadUserInfoTask.Result;
                    Logging.FirestoreLogger.Log("LoadUserInfo Successfully");
                }
                else
                {
                    Logging.FirestoreLogger.Log("LoadUserInfo has Exception: " + loadUserInfoTask.Exception);
                }
            });
    }
}
