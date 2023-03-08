using System;
using UnityEngine;
using UnityEngine.UIElements;
using Wonderland;

namespace Wonderland.GamePlay.Lobby
{
    public class LobbyUI : UIHandler
    {
        public static LobbyUI Instance;
        
        [Header("UI Assets")]
        public VisualTreeAsset mainLobbyUxml;
        public VisualTreeAsset logoutLobbyUxml;

        #region Lobby Components

        private Button logoutButton;
        private Button playButton;
        private Button yesButton;
        private Button noButton;
        
        #endregion

        #region Methods

        private void OnLogoutButtonClicked()
        {
            MainManager.Instance.uiManager.ChangeUxml(logoutLobbyUxml);
        }

        private void OnPlayButtonClicked()
        {
            MainManager.Instance.gameManager.LoadSceneWithLoaderAsync(GameManager.SceneType.NetRunning);
        }

        private void OnYesSignoutClicked()
        {
            //TODO: SignOut Process
        }

        private void OnNoSignoutClicked()
        {
            MainManager.Instance.uiManager.ChangeUxml(mainLobbyUxml);
        }

        private void OnUxmlChange()
        {
            VisualElement currentUxml = MainManager.Instance.uiManager.currentUxml;
            switch (MainManager.Instance.uiManager.GetCurrentUxmlName()){
                case "LobbyPanel" :
                    logoutButton = currentUxml.Q<Button>("Logout");
                    playButton = currentUxml.Q<Button>("Play");
                    logoutButton.clicked += OnLogoutButtonClicked;
                    playButton.clicked += OnPlayButtonClicked;
                    break;
                case "LogoutPanel" :
                    yesButton = currentUxml.Q<Button>("YesButton");
                    noButton = currentUxml.Q<Button>("NoButton");
                    yesButton.clicked += OnYesSignoutClicked;
                    noButton.clicked += OnNoSignoutClicked;
                    break;
            }
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
