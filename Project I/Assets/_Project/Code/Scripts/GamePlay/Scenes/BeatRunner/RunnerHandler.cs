using System;
using System.Collections;
using UnityEngine;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public class RunnerHandler : MonoBehaviour
    {
        [SerializeField] GameObject runnerPrefab;
        private Runner _runner;

        #region Methods

        public IEnumerator LoadRunnerCoroutine()
        {
            yield return null;
        }

        //private static async Task<Runner> InitializeRunner(Transform spawnTransform) => await 

        #endregion

        private void Awake()
        {
            throw new NotImplementedException();
        }

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
