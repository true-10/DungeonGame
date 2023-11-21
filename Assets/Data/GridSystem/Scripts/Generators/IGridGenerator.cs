using System;

namespace GridSystem
{
    public interface IGridGenerator 
    {
        Grid Generate(GridData gridData, Action<GridCell> onCellCreated = null);

    }
}
