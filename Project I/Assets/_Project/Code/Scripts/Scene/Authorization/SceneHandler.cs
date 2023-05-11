namespace Wonderland.Scene.Authorization
{
    public class SceneHandler : Management.SceneHandler
    {
        private void Start()
        {
            Management.UIHandler.ChangeUxml(UIHandler.Instance.SignUp);
        }
    }
}