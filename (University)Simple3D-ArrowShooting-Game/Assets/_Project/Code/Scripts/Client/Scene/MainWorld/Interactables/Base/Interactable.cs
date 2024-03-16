using System;
using UnityEngine;
using Wonderland.Types;

namespace Wonderland.Client.MainWorld
{
    [Serializable]
    public abstract class Interactable : Objects
    {
        [Header("Status")] 
        public bool isAvailable = true;
        [Header("Settings")]
        public new string name;
        public int id;
        public InteractionType type;
        [Header("Required Components")] 
        public Renderer meshRenderer;

        #region Methods

        public abstract void Interaction();

        #endregion
        
        public enum InteractionType {Item, Object}
    }
}
