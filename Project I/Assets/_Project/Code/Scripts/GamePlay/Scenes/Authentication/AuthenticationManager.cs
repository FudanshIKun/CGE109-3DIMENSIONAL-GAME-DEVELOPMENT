using UnityEngine;
using Wonderland.Auth;
using Wonderland.Manager;

namespace Wonderland.GamePlay.Authentication
{
    public class AuthenticationManager : MonoBehaviour
    {
        void Start()
        {
            UIManager.Instance.ChangeUxml(AuthUI.Instance.signInUxml);
        }
    }
}
