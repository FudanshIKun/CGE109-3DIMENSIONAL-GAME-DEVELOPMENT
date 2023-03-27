using System.Threading.Tasks;
using Codice.Client.Common;
using Firebase;
using Firebase.Auth;
using UnityEngine;

namespace Wonderland.API
{
    public static class AuthAPI
    {
        public static User CurrentUser { get; private set; }
        private static readonly FirebaseAuth Auth = FirebaseAuth.DefaultInstance;

        public static async Task SignUp(string userName, string email, string password)
        {
            await Auth.CreateUserWithEmailAndPasswordAsync(email, password);

            await Auth.SignInWithEmailAndPasswordAsync(email, password);

            if (IsSignedIn())
            {
                CurrentUser = new User(GetCurrentAuthUser())
                {
                    UserName = userName,
                    UserInfo =
                    {
                        ["UserName"] = userName
                    }
                };
                
                await FirestoreAPI.PostToFirestoreRequest(FirestoreAPI.UserInfoDocRef, CurrentUser.UserInfo).ContinueWith(
                    postUserInfoTask =>
                    {
                        if (postUserInfoTask.Exception != null)
                        {
                            Logging.FirestoreLogger.Log("Post User Information To Firestore Encounter Exception: " + postUserInfoTask.Exception);
                        }
                        else
                        {
                            Debug.LogFormat(
                                $"Successfully Post User: {CurrentUser.UserInfo["UserName"]}'s Information To Firestore");
                        }
                    });
                
                Debug.LogFormat(
                    $"Successfully SignUp User: {email}");
            }
        }

        public static async Task UpdateUserProfileRequest(UserProfile userProfile)
        {
            await Auth.CurrentUser.UpdateUserProfileAsync(userProfile);
        }

        public static async Task SignIn(string email, string password)
        {
            await Auth.SignInWithEmailAndPasswordAsync(email, password);

            if (IsSignedIn())
            {
                await FirestoreAPI.RetrieveFromFirestoreRequest(FirestoreAPI.UserInfoDocRef).ContinueWith(loadUserInfoTask =>
                {
                    if (loadUserInfoTask.Exception == null)
                    {
                        CurrentUser = new User(GetCurrentAuthUser())
                        {
                            UserName = loadUserInfoTask.Result["UserName"].ToString(),
                            DisplayName = loadUserInfoTask.Result["DisplayName"].ToString(),
                            UserInfo = loadUserInfoTask.Result
                        };
                        Logging.FirestoreLogger.Log("LoadUserInfo Successfully");
                    }
                    else
                    {
                        Logging.FirestoreLogger.Log("LoadUserInfo has Exception: " + loadUserInfoTask.Exception);
                    }
                });
                
                Debug.LogFormat(
                    $"Successfully SignIn User: {Auth.CurrentUser.Email} {Auth.CurrentUser.UserId}");
            }
        }

        public static void SignOutRequest()
        {
            Auth.SignOut();
            Debug.Log("Successfully SignOut User");
        }

        public static bool IsSignedIn() => Auth.CurrentUser != null;

        public static FirebaseUser GetCurrentAuthUser() => Auth.CurrentUser;

        public static User GetCurrentUser() => CurrentUser;
    }
}
