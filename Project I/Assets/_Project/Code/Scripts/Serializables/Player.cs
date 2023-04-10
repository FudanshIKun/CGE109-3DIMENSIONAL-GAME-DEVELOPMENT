using System;
using UnityEngine;

namespace Wonderland.Serializables
{
    [Serializable]
    public class Player : Types.Objects
    {
        [Header("Player Information")]
        public string userName;
        public string playerName; 

        public Player(string userName, string playerName)
        {
            
        }
    }
}