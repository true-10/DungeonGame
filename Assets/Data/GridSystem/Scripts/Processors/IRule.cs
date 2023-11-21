using UnityEngine;

namespace GridSystem
{
    public interface IRule
    {
        bool IsFollowed(CellInfo<GameObject> targetCellInfo);
    }
}
