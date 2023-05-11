using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wonderland.Scene.MainWorld
{
    public class Item : Interactable
    {
        public bool stackable;
        
        public override void Interaction()
        {
            CustomLog.GamePlaySystem.Log("Interaction with: " + this);
            isAvailable = false;
        }
    }
}
