using System;
using UnityEngine;

namespace Wonderland.GamePlay
{
    public class Cat3D : Cat
    {
        #region Fields

        [SerializeField] private Cat3DSetting cat3DSetting;

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

        #endregion

        #region Structs

        [Serializable]
        public struct Cat3DSetting
        {
            
        }

        #endregion
    }
}
