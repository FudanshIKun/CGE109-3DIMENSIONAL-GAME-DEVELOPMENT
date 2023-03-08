using UnityEngine;
using Wonderland;

namespace Wonderland.GamePlay.NetRunning
{
    public class GamePlayManager : SceneHandler
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

        private void Start()
        {
            if (MainManager.Instance.inputManager != null && MainManager.Instance.inputManager.gameObject.GetComponent<SwipeDetection>() != null)
            {
                SwipeDetection.LeftSwipe += _Runner.TurnLeft;
                SwipeDetection.RightSwipe += _Runner.TurnRight;
                SwipeDetection.UpSwipe += _Runner.Jump;
            }
        }

        private void OnDisable()
        {
            if (MainManager.Instance.inputManager != null && MainManager.Instance.inputManager.gameObject.GetComponent<SwipeDetection>() != null)
            {
                SwipeDetection.LeftSwipe -= _Runner.TurnLeft;
                SwipeDetection.RightSwipe -= _Runner.TurnRight;
                SwipeDetection.UpSwipe -= _Runner.Jump;
            }
        }
    }
}
