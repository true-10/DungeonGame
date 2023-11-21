using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public sealed class GridGenerator2D
    {
        public static Grid Generate(GridData gridData, Action<GridCell> onCellCreated = null)
        {

            Vector3Int gridSize = gridData.GridSize;
            Vector3 CellSize = gridData.Cellize;
            Vector3 startPos = Vector3.zero;
            Vector3 offset = gridData.Offset;
            var gridCells = new List<GridCell>();

            int index = 0;
            float xSize = CellSize.x;
            float ySize = CellSize.y;
            float zSize = CellSize.z;
            startPos.x = -gridSize.x / 2 * xSize - xSize / 2f;
            startPos.z = -gridSize.z / 2 * zSize + zSize / 2f;
            for (int z = 0; z < gridSize.z; z++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    int y = 0;
                    float xPos = x * xSize + xSize + offset.x;
                    float yPos = y * ySize + ySize + offset.y;
                    float zPos = z * zSize + offset.z * (x % 2) * zSize;
                    Vector3 newPos = startPos + new Vector3(xPos, yPos, zPos);
                    Vector3Int coords = new Vector3Int(x, y, z);
                    GridCell cell = new GridCell(index, newPos, CellSize, coords);
                    cell.CalculateBounds();
                    gridCells.Add(cell);
                    onCellCreated?.Invoke(cell);
                    index++;
                }
            }

            Grid grid = new(gridData, gridCells);
            return grid;
        }
    }
}
