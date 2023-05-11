using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Wonderland.Scene.MainWorld
{
    public class DragArea : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [ReadOnly] public Vector2 DragAmount { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            DragAmount = eventData.delta;
        }

        public void OnDrag(PointerEventData eventData)
        {
            DragAmount = eventData.delta;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            DragAmount = Vector2.zero;
        }
    }
}
