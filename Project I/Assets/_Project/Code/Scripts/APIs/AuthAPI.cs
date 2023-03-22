using System.Threading.Tasks;
using Firebase.Auth;
using UnityEngine;

namespace Wonderland.API
{
    public static class AuthAPI
    {
        private static readonly FirebaseAuth Auth = FirebaseAuth.DefaultInstance;

        public static async Task SignUpRequest(string email, string password)
        {
            await Auth.CreateUserWithEmailAndPasswordAsync(email, password);
            Debug.LogFormat(
                $"Successfully SignUp User: {email}");
        }

        public static async Task UpdateUserProfileRequest(UserProfile userProfile)
        {
            await Auth.CurrentUser.UpdateUserProfileAsync(userProfile);
        }

        public static async Task SignInRequest(string email, string password)
        {
            await Auth.SignInWithEmailAndPasswordAsync(email, password);
            Debug.LogFormat(
                $"Successfully SignIn User: {Auth.CurrentUser.Email} {Auth.CurrentUser.UserId}");
        }

        public static void SignOutRequest()
        {
            Auth.SignOut();
            Debug.Log("Successfully SignOut User");
        }

        public static bool IsSignedIn() => Auth.CurrentUser != null;

        public static FirebaseUser GetAuthCurrentUser() => Auth.CurrentUser;
    }
}
