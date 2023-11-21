using GridSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GridSystem
{

    public sealed class AvailableCellsProcessor
    {
        private IGridController gridController;
        private GridInfoManager<GameObject> gridInfoManager;

        private GridInfo<GameObject> gridInfo => gridInfoManager.GridInfo;
        private Grid grid => gridController.Grid;
        private List<GridCell> availableCells;
        private List<IRule> rules;

        public AvailableCellsProcessor(IGridController gridController, GridInfoManager<GameObject> gridInfoManager, List<IRule> rules)
        {
            this.gridController = gridController;
            this.gridInfoManager = gridInfoManager;
            this.rules = rules;
        }

        public void CalculateAvailableCellsFrom(GridCell gridCell, int length)
        {
            availableCells = new();
            for (int x = -length; x <= length; x++)
            {
                for (int y = -length; y <= length; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }
                    AddCell(x, y, gridCell);
                }

            }
            gridInfoManager.GridInfo.AvailableCells = availableCells;
        }

        private void AddCell(int xOffset, int yOffset, GridCell curretnCell)
        {
            var xInd = curretnCell.Coordinates.x + xOffset;
            var yInd = curretnCell.Coordinates.z + yOffset;
            var cell = grid.GetCellFromIndicies(xInd, yInd);
            var cellInfo = gridInfo.GetCellInfo(cell);
            var currentCellInfo = gridInfo.GetCellInfo(curretnCell);
            AddCellToList(cellInfo,currentCellInfo);

        }

        private void AddCellToList(CellInfo<GameObject> targetCellInfo, CellInfo<GameObject> currentCellInfo)
        {
            if (targetCellInfo == null)
            {
                return;
            }
            if (targetCellInfo.IsEmpty)
            {
                availableCells.Add(targetCellInfo.GridCell);
            }
            else
            {
                if(rules.All(x => x.IsFollowed(targetCellInfo) ))
                {
                    availableCells.Add(targetCellInfo.GridCell);
                }
            }
        }
    }
}
