using True10.Prototyping;
using UnityEngine;
using Zenject;

namespace GridSystem
{
    public class GridVisualizer : MonoBehaviour
    {
        [Inject]
        private IGridController gridController;
        [Inject]
        private GridInfoManager<GameObject> gridInfoManager;

        [SerializeField, Header("Selectable Object")]
        private Transform selectableCell;
        [SerializeField]
        private Vector3 selectableOffset = Vector3.up;

        [SerializeField]
        private GameObject cellPrefab;
        [SerializeField]
        private Vector3 offset = Vector3.one;

        [SerializeField]
        private ObjectSpawner cellObjects;

        private VisualSelectableCell<GameObject> visualSelectableCell;

        void Start()
        {
            visualSelectableCell = new VisualSelectableCell<GameObject>(selectableCell, gridController, gridInfoManager, selectableOffset);
            SpawnCells();
        }

        private void SpawnCells()
        {
            if (cellPrefab == null)
            {
                return;
            }
            var cells = gridController.Grid.Cells;
            foreach (var cell in cells)
            {
                cellObjects.Spawn(cellPrefab, cell.Position + offset, Quaternion.identity, null);
            }
        }

        private void OnDestroy()
        {
            cellObjects?.Clear();
        }
    }
}
