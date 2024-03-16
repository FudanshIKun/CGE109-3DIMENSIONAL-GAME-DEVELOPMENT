using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;
using Wonderland.Management;

namespace Wonderland.Client.MainWorld
{
    public class UIHandler : Client.UIHandler
    {
        public static UIHandler Instance;
        
        [Header("Status")]
        
        [Header("Settings")]
        [Header("Required Components")]
        [SerializeField] private Canvas gameplayCanvas;
        [Header("Uxml Assets")]
        [SerializeField] private VisualTreeAsset mainMenuUxml;
        
        protected override void Awake()
        {
            base.Awake();
            
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        #region Methods

        private void OnUxmlChange()
        {
            
        }

        #endregion

        protected void OnEnable()
        {
            
        }

        protected void OnDisable()
        {
            
        }
    }
}