using GridSystem;
using UnityEngine;

namespace GridSystem
{
    public sealed class VisualSelectableCell<T>
    {
        private Transform selectable;
        private IGridController gridController;
        private GridInfoManager<T> gridInfoManager;

        private Vector3 offset;

        public VisualSelectableCell(Transform transform, IGridController gridController, GridInfoManager<T> gridInfoManager, Vector3 offset)
        {
            this.selectable = transform;
            this.gridController = gridController;
            this.gridInfoManager = gridInfoManager;
            this.offset = offset;

            gridController.OnCellOver += OnCellInput;
        }

        private void OnCellInput(GridCell cell)
        {
            if (cell == null)
            {
                return;
            }
            var availableCells = gridInfoManager.GridInfo.AvailableCells;

            if (availableCells.Contains(cell))
            {
                selectable.position = cell.Position + offset;
            }
            else
            {
                selectable.position = Vector3.down * 10f;

            }          
        }
    }
}
