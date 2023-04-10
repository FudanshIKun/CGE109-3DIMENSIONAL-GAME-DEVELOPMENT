using System.Threading.Tasks;
using UnityEngine;

namespace Wonderland.Management
{
    public abstract class GameplayHandler : MonoBehaviour
    {
        public bool GameplayReady { get; protected set; }
        
        public abstract Task SetUpGameplay();
        protected virtual void OnEnable()
        {
            MainManager.Instance.GamePlayHandler = this;
        }

        protected virtual void OnDisable()
        {
            MainManager.Instance.GamePlayHandler = null;
        }
    }
}
