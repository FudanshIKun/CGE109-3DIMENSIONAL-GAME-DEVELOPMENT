using System;
using UnityEngine;
using UnityEngine.UIElements;
using Wonderland.Manager;

namespace Wonderland.UserInterface
{
    public class LobbyUI : MonoBehaviour
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
            UIManager.Instance.ChangeUxml(logoutLobbyUxml);
        }

        private void OnPlayButtonClicked()
        {
            GameManager.Instance.LoadSceneWithLoaderAsync(GameManager.SceneType.KittyRun);
        }

        private void OnYesSignoutClicked()
        {
            //TODO: SignOut Process
        }

        private void OnNoSignoutClicked()
        {
            UIManager.Instance.ChangeUxml(mainLobbyUxml);
        }

        private void OnUxmlChange()
        {
            VisualElement currentUxml = UIManager.Instance.currentUxml;
            switch (UIManager.Instance.GetCurrentUxmlName()){
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
