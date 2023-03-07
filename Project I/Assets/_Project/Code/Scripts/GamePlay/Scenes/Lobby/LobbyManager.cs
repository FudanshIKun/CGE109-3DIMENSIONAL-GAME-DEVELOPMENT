using UnityEngine;
using Wonderland;

namespace Wonderland.GamePlay.Lobby
{
    public class LobbyManager : MonoBehaviour
    {
        #region Methods
        
        private void SetLobby()
        {
            //TODO: Instantiate Furniture In Room By Furniture Position From DataManager
        }
        
        private void LoadCats()
        {
            //TODO: Instantiate Cats In Scene At Each CatStation Furniture Type By Get Data Of Amount Of Cat From DataManager
        }

        public void LoadProfilePicture()
        {
            //TODO: Load User's Profile Picture from DataManager
        }

        #endregion

        private void Start()
        {
            MainManager.Instance.uiManager.ChangeUxml(LobbyUI.Instance.mainLobbyUxml);
        }
    }
}
