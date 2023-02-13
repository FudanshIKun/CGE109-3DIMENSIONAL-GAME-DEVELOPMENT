using UnityEngine;
using Wonderland.Manager;
using Wonderland.Interface;

namespace Wonderland.GamePlay.Lobby
{
    public class LobbyManager : MonoBehaviour
    {
        #region Methods
        
        private void SetRoom()
        {
            //TODO: Instantiate Furniture In Room By Furniture Position From DataManager
        }
        
        private void InstantiateCats()
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
            UIManager.Instance.ChangeUxml(LobbyUI.Instance.lobbyUxml);
        }
    }
}
