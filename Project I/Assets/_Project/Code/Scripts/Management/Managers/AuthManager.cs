using System.Threading.Tasks;
using Wonderland.API;

namespace Wonderland.Management
{
    public class AuthManager : IManager
    {
        public static User CurrentUser;
        
        public static async Task SignUp(string userName, string email, string password)
        {
            await AuthAPI.SignUpRequest(email, password);

            await AuthAPI.SignInRequest(email, password);

            CurrentUser = new User(AuthAPI.GetAuthCurrentUser())
            {
                UserInfo =
                {
                    ["UserName"] = userName
                }
            };

            await FirestoreManager.PostUserInfoToFireStore(CurrentUser);
        }
        
        public static async Task SignIn(string email, string password)
        {
            await AuthAPI.SignInRequest(email, password);
            
            CurrentUser = new User(AuthAPI.GetAuthCurrentUser());
            
            await FirestoreManager.LoadUserInfoFromFirestore();
        }

        // public static async Task SignOutAsync()
        // {
        //     
        // }
    }
}
