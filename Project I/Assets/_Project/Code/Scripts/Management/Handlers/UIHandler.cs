using UnityEngine;

namespace Wonderland.Management
{
    public abstract class UIHandler : MonoBehaviour
    {
        protected abstract void OnUxmlChange();
        
        protected virtual void OnEnable()
        {
            MainManager.Instance.UIHandler = this;
            UIManager.UxmlChanged += OnUxmlChange;
        }
        
        protected virtual void OnDisable()
        {
            MainManager.Instance.UIHandler = null;
            UIManager.UxmlChanged -= OnUxmlChange;
        }
    }
}
