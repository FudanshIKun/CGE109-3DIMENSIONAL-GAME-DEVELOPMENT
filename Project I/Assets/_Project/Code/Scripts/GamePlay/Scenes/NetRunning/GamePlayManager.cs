using UnityEngine;

namespace Wonderland.GamePlay.NetRunning
{
    public class GamePlayManager : MonoBehaviour
    {
        public static GamePlayManager Instance;
        
        #region Fields

        private Player _Player;
        public Runner _Runner;

        #endregion
        
        #region Methods

        public void LoadCharacter()
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
            _Player = GameObject.FindWithTag("Player").GetComponent<Player>();
            _Runner = GameObject.FindWithTag("Player").GetComponent<Runner>();
        }
    }
}
