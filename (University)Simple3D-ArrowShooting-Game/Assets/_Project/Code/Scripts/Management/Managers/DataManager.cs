using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Wonderland.Management
{
    public class DataManager : SerializedMonoBehaviour
    {
        public static DataManager Instance { get; private set; }
        
        private static Dictionary<string, object> UserInfo;
        private static Dictionary<string, object> GameData;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #region Methods

        

        #endregion
    }
}
