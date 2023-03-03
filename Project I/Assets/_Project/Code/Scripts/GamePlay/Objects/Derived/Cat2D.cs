using System;
using UnityEngine;

namespace Wonderland.GamePlay
{
    public class Cat2D : Cat
    {
        #region Fields

        [SerializeField] private Cat2DSetting CatSetting;

        #endregion

        #region Methods

        public virtual void TouchInteraction()
        {
            if (interactionSetting.isTouchable == false)
            {
                return;
            }
        }

        public virtual void TouchCancleInteraction()
        {
            if (interactionSetting.isTouchable == false)
            {
                return;
            }
        }
        
        public virtual void Swiped()
        {
            if (interactionSetting.isSwipable == false)
            {
                return;
            }      
        }

        #endregion

        #region Structs

        [Serializable]
        public struct Cat2DSetting
        {
            [Header("Sprite Setting")]
            [SerializeField] private Sprite catSprite;
            [SerializeField] private Animator catAnimator;
        
            
        }

        #endregion
    }
}
