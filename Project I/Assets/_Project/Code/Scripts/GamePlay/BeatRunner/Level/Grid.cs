using UnityEngine;

namespace Wonderland.GamePlay.BeatRunner
{
    public class Grid
    {
        #region Fields

        //Grid
        private readonly Vector3 _gridTransform;
        
        //Cell
        private readonly int _cellLength;
        private readonly int _cellAmount;

        #endregion

        public Grid(Vector3 gridTransform,int sectionWidth, int sectionLength, int sectionHeight, int cellAmounts)
        {
            _gridTransform = gridTransform;
            _cellAmount = cellAmounts;
            _cellLength = sectionLength / cellAmounts;
            
            Logging.GamePlayLogger.Log("Has Create New Grid At " + _gridTransform);
            int lastZ = (int)gridTransform.z + sectionLength;
            SceneHandler.Instance.GameplayHandler.LastZ = new Vector3(gridTransform.x, gridTransform.y, lastZ);
        }
        

        public int CurrentCell(Types.Objects anyObject)
        {
            var zTarget = (int)anyObject.transform.position.z;
            var zStart = (int)_gridTransform.z;

            for (var i = 1; i <= _cellAmount; i++)
            {
                if (anyObject.IsBetween(zTarget, zStart, zStart + _cellLength * i))
                {
                    return i;
                }
            }

            return 0;
        }
    }
}