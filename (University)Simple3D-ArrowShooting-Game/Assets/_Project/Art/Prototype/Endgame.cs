using UnityEngine;
using UnityEngine.EventSystems;

namespace Wonderland
{
    public class Endgame : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Application.Quit();
        }
    }
}
