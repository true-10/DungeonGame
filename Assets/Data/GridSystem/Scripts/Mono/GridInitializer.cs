using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GridSystem
{
    public class GridInitializer : MonoBehaviour
    {
        [Inject]
        private IGridController gridController;
        [Inject]
        private GridInfoManager<GameObject> gridInfoManager;

        void Awake()
        {
            gridInfoManager.Init(gridController.Grid.Cells);
        }
    }
}
