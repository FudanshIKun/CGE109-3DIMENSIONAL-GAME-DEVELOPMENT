using System;
using UnityEngine;
using Wonderland.Types;

namespace Wonderland.GamePlay.BeatRunner
{
    [Serializable][RequireComponent(typeof(MeshRenderer))]
    public class Section : PoolObject
    {
        [Header("Section Settings")]
        [SerializeField] private int sectionWidth;
        [SerializeField] private int sectionLength;
        [SerializeField] private int sectionHeight;
        public enum SectionType
        {
            FullSection,
            HalfSection
        }

        public SectionType type;
        
        [Header("Cell Settings")]
        private int _cellAmount = 6;

        [HideInInspector] public bool firstCellHasInvoked;
        [HideInInspector] public bool secondCellHasInvoked;
        [HideInInspector] public bool thirdCellHasInvoked;
        [HideInInspector] public bool fourthCellHasInvoked;
        [HideInInspector] public bool fifthCellHasInvoked;
        [HideInInspector] public bool sixthCellHasInvoked;

        private Grid CurrentGrid { get; set; }

        #region Methods

        public void CreateNewGrid()
        {
            var position = transform.position;
            
            CurrentGrid = new Grid(position, sectionWidth, sectionLength, sectionHeight, _cellAmount);

            SceneHandler.Instance.GameplayHandler.LastZ = new Vector3(position.x, position.y,
                (int)position.z + sectionLength);
        }
        

        public int CurrentCell(Types.Objects anyObject)
        {
            return CurrentGrid.CurrentCell(anyObject);
        }

        #endregion
    }
}