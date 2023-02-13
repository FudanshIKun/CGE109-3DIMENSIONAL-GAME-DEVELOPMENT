using System;
using UnityEngine;
using UnityEngine.UIElements;
using Wonderland.Manager;

namespace Wonderland.Interface
{
    public class LobbyUI : SceneUI
    {
        public static LobbyUI Instance;
        
        [Header("UI Assets")]
        public VisualTreeAsset lobbyUxml;

        #region Methods

        private void OnUxmlChange()
        {
            
        }

        #endregion

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void OnEnable()
        {
            UIManager.UxmlChanged += OnUxmlChange;
        }

        private void OnDisable()
        {
            UIManager.UxmlChanged -= OnUxmlChange;
        }
    }
}
