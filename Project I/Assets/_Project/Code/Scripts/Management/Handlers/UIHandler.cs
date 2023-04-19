using UnityEngine;

namespace Wonderland.Management
{
    public abstract class UIHandler : MonoBehaviour
    {
        protected abstract void OnUxmlChange();
        
        protected virtual void OnEnable()
        {
            UIManager.UxmlChanged += OnUxmlChange;
        }
        
        protected virtual void OnDisable()
        {
            UIManager.UxmlChanged -= OnUxmlChange;
        }
    }
}
