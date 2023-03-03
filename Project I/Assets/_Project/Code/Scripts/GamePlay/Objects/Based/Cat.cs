using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wonderland.GamePlay
{
    public class Cat : MonoBehaviour
    {
        #region Fields

        [Header("Setting")]
        public CatData catData;
        public InteractionSetting interactionSetting;

        #endregion

        #region Structs

        [Serializable]
        public struct CatData
        {
            public string catName;
            public string catID;
            public CatSpecies CatSpecies;
            public CatType catType;
            public List<Ability> CatAbilities;
        }
        
        [Serializable]
        public struct InteractionSetting
        {
            public bool isTouchable;
            public bool isSwipable;
        }

        #endregion

        #region enums

        public enum CatSpecies
        {
            
        }

        public enum CatType
        {
            
        }

        #endregion
    }
}
