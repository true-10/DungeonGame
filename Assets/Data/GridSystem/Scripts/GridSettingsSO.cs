using UnityEngine;

namespace GridSystem
{
    [CreateAssetMenu(menuName = "Grid System/Grid Settings")]
    public class GridSettingsSO : ScriptableObject
    {
        [Header("Grid Data")]
        public Vector3Int GridSize = Vector3Int.one * 5;
        public Vector3Int SectorSize = Vector3Int.one * 3;
        public Vector3 Offset = Vector3.zero;
        public Vector3 CellSize = Vector3.one;
        public GridType GridType = GridType.Square;
    }
}
