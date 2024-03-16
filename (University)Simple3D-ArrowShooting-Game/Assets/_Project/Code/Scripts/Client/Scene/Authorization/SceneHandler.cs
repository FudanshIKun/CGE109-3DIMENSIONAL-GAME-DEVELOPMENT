using Wonderland.Management;

namespace Wonderland.Client.Authorization
{
    public class SceneHandler : Client.SceneHandler
    {
        private void Start()
        {
            StartCoroutine(NetworkManager.TryConnection(isConnected =>
            {
                if (isConnected)
                {
                    CustomLog.Network.Log("Internet Is Available!");
                    Client.UIHandler.ChangeUxml(UIHandler.Instance.SignIn);
                }
                else
                {
                    CustomLog.Network.Log("Internet Is Not Available");
                    Client.UIHandler.ChangeUxml(UIHandler.Instance.ConnectionFailed);
                }
            }));
        }
    }
}