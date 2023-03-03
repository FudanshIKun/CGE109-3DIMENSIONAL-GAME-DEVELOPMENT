using System;
using System.Collections;
using UnityEngine;
using Wonderland.Utility;

namespace Wonderland.GamePlay.Lobby
{
    public class LobbyCat : Cat2D, ITouchable
    {
        #region Fields

        private SpriteRenderer _sprite;
        private Animator _animator;

        #endregion
        
        #region Methods

        public override void TouchInteraction()
        {
            base.TouchInteraction();
            Logging.GamePlayLogger.Log("Touch Interacting With " + catData.catName);
        }

        public override void TouchCancleInteraction()
        {
            base.TouchCancleInteraction();
            Logging.GamePlayLogger.Log("Touch Cancle With " + catData.catName);
        }

        #endregion

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }
    }
}
