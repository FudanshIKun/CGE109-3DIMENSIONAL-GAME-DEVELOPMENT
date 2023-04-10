using UnityEngine;
using UnityEngine.UIElements;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public class UIHandler : Management.UIHandler
    {
        [Header("UI Assets")]
        [SerializeField] private VisualTreeAsset mainMenuUxml;

        #region Components

        

        #endregion

        #region Methods

        protected override void OnUxmlChange()
        {
            //TODO: Assign Value On Event UIManager.UxmlChanged
        }

        #endregion

        protected override void OnEnable()
        {
            
        }

        protected override void OnDisable()
        {
            
        }
    }
}