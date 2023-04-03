using System;
using UnityEngine;
using UnityEngine.Events;
using Wonderland.Management;

namespace Wonderland.GamePlay.BeatRunner
{
    public class BeatRunnerGameplayHandler : MonoBehaviour
    {
        #region Prefabs Setting

        [Header("Prefabs")]
        public GameObject fullSection;
        public GameObject halfSection;

        #endregion
        
        #region Level Generator Settings

        [Header("Level Generation")]
        public int sectionLength;
        public int cellAmount;
        public static Section CurrentSection { get; set; }

        #endregion
        
        #region Fields

        public static Transform RunnerTransform { get; private set; }
        private GameObject Environment { get; set; }

        #endregion

        #region Methods

        private void GenerateSection()
        {
            GameObject nextSection;
        }

        #endregion

        private void Awake()
        {
            Environment = GameObject.FindGameObjectWithTag("Environment");
            RunnerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }
    }
}
