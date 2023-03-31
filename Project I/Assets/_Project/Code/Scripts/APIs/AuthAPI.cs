using System.Threading.Tasks;
using Firebase.Auth;
using UnityEngine;

namespace Wonderland.API
{
    public static class AuthAPI
    {
        public static async Task SignUp(FirebaseAuth auth, string email, string password)
        {
            await auth.CreateUserWithEmailAndPasswordAsync(email, password);

            await auth.SignInWithEmailAndPasswordAsync(email, password);
        }

        public static async Task UpdateUserProfileRequest(FirebaseAuth auth, UserProfile userProfile)
        {
            await auth.CurrentUser.UpdateUserProfileAsync(userProfile);
        }

        public static async Task SignIn(FirebaseAuth auth, string email, string password)
        {
            await auth.SignInWithEmailAndPasswordAsync(email, password);
        }

        public static void SignOut(FirebaseAuth auth)
        {
            auth.SignOut();
            Debug.Log("Successfully SignOut User");
        }
    }
}
