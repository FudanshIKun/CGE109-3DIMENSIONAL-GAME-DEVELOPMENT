using System;
using UnityEngine;
using UnityEngine.Events;
using Object = Wonderland.Objects.Object;

namespace Wonderland.GamePlay.BeatRunner
{
    [Serializable] public class SectionEvent : UnityEvent<string, GameObject> {}
    
    [RequireComponent(typeof(MeshRenderer))]
    public class Section : Object
    {
        [Header("Cell Settings")] 
        [SerializeField] int sectionLength;
        [SerializeField] int cellLength;
        
        [Header("Cell Events")] 
        [SerializeField] SectionEvent eventsCell0;
        [SerializeField] SectionEvent eventsCell1;
        [SerializeField] SectionEvent eventsCell2;
        [SerializeField] SectionEvent eventsCell3;
        [SerializeField] SectionEvent eventsCell4;
        
        public Grid CurrentGrid { get; private set; }

        #region Method

        public int CurrentCell() => CurrentGrid.DetermineWhichCellTargetIsIn(BeatRunnerGameplayHandler.RunnerTransform.position);

        #endregion

        private void Awake()
        {
            CurrentGrid = new Grid(transform.position, sectionLength, cellLength);
        }
    }
}
