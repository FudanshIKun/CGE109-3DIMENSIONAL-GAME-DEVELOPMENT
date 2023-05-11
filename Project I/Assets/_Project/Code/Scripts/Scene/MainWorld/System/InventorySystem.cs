using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Wonderland.Scene.MainWorld
{
    internal class InventorySystem : System
    {
        [Header("Status")]
        [ReadOnly] public readonly Dictionary<int, Item> inventory = new();
        [Header("Settings")] 
        public int maxSize;

        public void Add(Item item)
        {
             
        }

        public void RemoveItem(Item item)
        {
            
        }

        public void CreateInventory()
        {
            
        }
    }
}
