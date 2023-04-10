using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public class SceneHandler : Management.SceneHandler
    {
        public static SceneHandler Instance { get; private set; }
        
        #region Handlers Fields

        public GameplayHandler GameplayHandler { get; private set; }
        public PlayerHandler PlayerHandler { get; private set; }
        public UIHandler UIHandler { get; private set; }

        #endregion

        #region Scene Fields

        public PlayableDirector mainDirector;
 
        #endregion

        #region Methods

        public override Task SetUpScene()
        {
            void Action()
            {
                
            }
              
            var setUpTask = new Task(Action);
            return setUpTask;
        }

        public override async void LaunchingGame()
        {
            await SetUpScene();
            
            await GameplayHandler.SetUpGameplay();

            if (GameplayHandler.GameplayReady) GameManager.ChangeGameState(IManager.State.PlayState);
        }

        #endregion

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                GameplayHandler = gameObject.GetComponentInChildren<GameplayHandler>();
                PlayerHandler = gameObject.GetComponentInChildren<PlayerHandler>();
                UIHandler = gameObject.GetComponentInChildren<UIHandler>();
                mainDirector = gameObject.GetComponentInChildren<PlayableDirector>();        
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        protected override void OnEnable()
        {
            
        }

        private void Start()
        {
            
        }
        
        protected override void OnDisable()
        {
            
        }
    }
}