using UnityEngine;

namespace Wonderland.GamePlay.BeatRunner
{
    public class Grid
    {
        public int CellLength { get; }
        public int NumberOfCells { get;}

        public Grid(Vector3 gridTransform, int sectionLength, int cellAmounts)
        {
            NumberOfCells = cellAmounts;
            CellLength = sectionLength / cellAmounts;
        }

        public int DetermineWhichCellTargetIsIn(Vector3 targetPosition)
        {
            Vector2 positionOnCell = new Vector2(targetPosition.x, targetPosition.z);
            
            return 0;
        }
    }
}
