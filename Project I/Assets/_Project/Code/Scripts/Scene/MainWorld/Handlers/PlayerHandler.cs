using Sirenix.OdinInspector;
using UnityEngine;

namespace Wonderland.Scene.MainWorld
{
    public class PlayerHandler : SerializedMonoBehaviour
    {
        [Header("Gameplay")]
        [SerializeField] private Player player;

        private void Start()
        {
            //GameplayHandler.Instance.Player = player;
        }
    }
}
