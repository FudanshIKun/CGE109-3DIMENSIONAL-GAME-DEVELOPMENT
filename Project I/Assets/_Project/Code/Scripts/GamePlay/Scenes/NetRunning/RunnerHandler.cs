using System;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Wonderland.GamePlay.NetRunning
{
    public class RunnerHandler : MonoBehaviour
    {
        [SerializeField] GameObject runnerPrefab;
        private Runner _runner;

        #region Methods

        private Task InitializeRunner()
        {
            //TODO: Instantiate Runner By The Data Retrieved From Database And Set This._runner To The Instantiated One
        }

        #endregion

        private void OnEnable()
        {
            if (MainManager.Instance.inputManager != null && MainManager.Instance.inputManager.gameObject.GetComponent<SwipeDetection>() != null)
            {
                SwipeDetection.LeftSwipe += _runner.TurnLeft;
                SwipeDetection.RightSwipe += _runner.TurnRight;
                SwipeDetection.UpSwipe += _runner.Jump;
            }
        }

        private void Start()
        {
            
        }

        private void OnDisable()
        {
            if (MainManager.Instance.inputManager != null && MainManager.Instance.inputManager.gameObject.GetComponent<SwipeDetection>() != null)
            {
                SwipeDetection.LeftSwipe -= _runner.TurnLeft;
                SwipeDetection.RightSwipe -= _runner.TurnRight;
                SwipeDetection.UpSwipe -= _runner.Jump;
            }
        }
    }
}
