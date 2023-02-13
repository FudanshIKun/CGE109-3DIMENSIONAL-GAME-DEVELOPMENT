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

        #region Lobby Components

        private Button LogoutButton;
        private Button PlayButton;

        #endregion

        #region Methods

        private void OnLogoutButtonClicked()
        {
            
        }

        private void OnPlayButtonClicked()
        {
            
        }

        private void OnUxmlChange()
        {
            LogoutButton = UIManager.Instance.currentUxml.Q<Button>("Logout");
            PlayButton = UIManager.Instance.currentUxml.Q<Button>("Play");
            LogoutButton.clicked += OnLogoutButtonClicked;
            PlayButton.clicked += OnPlayButtonClicked;
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
