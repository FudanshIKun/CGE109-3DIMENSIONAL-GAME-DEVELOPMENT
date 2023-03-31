using System;
using UnityEngine;

namespace Wonderland
{
    [Serializable]
    public class Player : MonoBehaviour
    {
        [Header("Player Information")]
        public string userName;
        public string playerName;

        public Player(string userName, string playerName)
        {
            
        }
    }
}
