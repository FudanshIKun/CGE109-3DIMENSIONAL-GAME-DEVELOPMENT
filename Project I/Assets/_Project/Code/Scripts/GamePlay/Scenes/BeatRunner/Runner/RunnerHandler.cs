using System;
using System.Collections;
using UnityEngine;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public class RunnerHandler : MonoBehaviour
    {
        public static GameObject RunnerPrefab;
        private Runner _runner;

        #region Methods

        public IEnumerator LoadRunnerCoroutine()
        {
            yield return null;
        }

        //private static async Task<Runner> InitializeRunner(Transform spawnTransform) => await 

        #endregion
    }
}
