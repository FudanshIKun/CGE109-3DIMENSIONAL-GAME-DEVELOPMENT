using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Wonderland.Client.MainWorld
{
    public class Dialogue :  MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            GameplayHandler.Instance.NextDialogue();
        }
    }
}