using UnityEngine;
using Zenject;

namespace GridSystem
{
    public class GridInstaller : MonoInstaller
    {
        [SerializeField]
        private bool initFromSettings = false;

        public override void InstallBindings()
        {
            Container.Bind<GridInfoManager<GameObject>>().AsSingle();

            if (initFromSettings)
            {
                Container.Bind<IGridController>().FromMethod(GetGridController).AsSingle();
            }
            else
            {
                Container.Bind<IGridController>().To<GridController>().AsSingle();
            }
        }

        private IGridController GetGridController()
        {
            GridSettingsSO gridSettings = GridSettingsStaticLoader.LoadSettings();
            var gridData = new GridData()
            {
                GridSize = gridSettings.GridSize,
                GridType = GridType.Square,
                Offset = gridSettings.Offset,
                Cellize = gridSettings.CellSize,
                SectorSize = gridSettings.SectorSize,
            };
            var grid = GridGenerator2D.Generate(gridData);
            return new GridController(grid);
        }
    }
}