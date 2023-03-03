using System;
using UnityEngine;

namespace Wonderland.GamePlay.KittyRun
{
    public class KittyRunGamePlayManager : MonoBehaviour
    {
        public static KittyRunGamePlayManager Instance;
        
        #region Fields

        public KittyRunCat kitty;

        #endregion
        
        #region Methods

        public void LoadGamePlay()
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
            kitty = GameObject.FindWithTag("Player").GetComponent<KittyRunCat>();
        }
    }
}
