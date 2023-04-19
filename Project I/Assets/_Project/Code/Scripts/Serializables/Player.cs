using System;
using UnityEngine;

namespace Wonderland.Serializables
{
    [Serializable]
    public class Player
    {
        [Header("Player Information")]
        public string userName;
        public string playerName; 

        public Player(string userName, string playerName)
        {
            
        }
    }
}