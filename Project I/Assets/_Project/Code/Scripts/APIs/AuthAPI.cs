using System.Threading.Tasks;
using Firebase.Auth;

namespace Wonderland
{
    public static class AuthAPI
    {
        private static readonly FirebaseAuth Auth = FirebaseAuth.DefaultInstance;
        
        public static async Task<RequestState> SignUpRequest(string email, string password) =>
            await Auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                    return new RequestState().MakeFaulted("New user creation was canceled.");

                if (task.IsFaulted)
                    return new RequestState().MakeFaulted(task.Exception?.GetBaseException().Message);

                return new RequestState();
            });
        
        public static async Task<RequestState> SignInRequest(string email, string password) =>
            await Auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                    return new RequestState().MakeFaulted("Sign in was canceled.");

                if (task.IsFaulted)
                    return new RequestState().MakeFaulted(task.Exception?.GetBaseException().Message);

                return new RequestState();
            });

        //public static async Task<RequestState> SignOutRequest()

        public static bool IsSignedIn() => Auth.CurrentUser != null;

        public static FirebaseUser GetAuthUser() => Auth.CurrentUser;

        public static void SignOut() => Auth.SignOut();
    }
}
