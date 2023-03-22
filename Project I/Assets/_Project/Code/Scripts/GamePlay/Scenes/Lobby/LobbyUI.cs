using System;
using UnityEngine;
using UnityEngine.UIElements;
using Wonderland.Management;

namespace Wonderland.GamePlay.Lobby
{
    public class LobbyUI : UIHandler
    {
        public static LobbyUI Instance;
        
        [Header("UI Assets")]
        public VisualTreeAsset mainLobbyUxml;
        public VisualTreeAsset logoutLobbyUxml;

        #region Lobby Components

        private Button _logoutButton;
        private Button _playButton;
        private Button _yesButton;
        private Button _noButton;
        
        #endregion

        #region Methods

        private void OnLogoutButtonClicked()
        {
            MainManager.Instance.uiManager.ChangeUxml(logoutLobbyUxml);
        }

        private void OnPlayButtonClicked()
        {
            MainManager.Instance.gameManager.LoadSceneWithLoaderAsync(IManager.SceneType.BeatRunner);
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
            VisualElement currentUxml = MainManager.Instance.uiManager.CurrentUxml;
            switch (MainManager.Instance.uiManager.GetCurrentUxmlName()){
                case "LobbyPanel" :
                    _logoutButton = currentUxml.Q<Button>("Logout");
                    _playButton = currentUxml.Q<Button>("Play");
                    _logoutButton.clicked += OnLogoutButtonClicked;
                    _playButton.clicked += OnPlayButtonClicked;
                    break;
                case "LogoutPanel" :
                    _yesButton = currentUxml.Q<Button>("YesButton");
                    _noButton = currentUxml.Q<Button>("NoButton");
                    _yesButton.clicked += OnYesSignoutClicked;
                    _noButton.clicked += OnNoSignoutClicked;
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
