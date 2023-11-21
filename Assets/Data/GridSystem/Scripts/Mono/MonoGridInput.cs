using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace GridSystem
{
    public class MonoGridInput : MonoBehaviour
    {
        [Inject]
        private IGridController gridController;
        [Inject]
        private GridInfoManager<GameObject> gridInfoManager;

        [SerializeField, Header("Raycast")]
        private string layerName = "Grid";
        [SerializeField]
        private float raycastDistance = 1000f;

        private IGridInput gridInput;
        public IGridInput GridInput => gridInput;
        public bool Disable = false;

        void Awake()
        {
            gridInput = new RaycastGridInput(gridController, gridLayerName: layerName, raycastDistance);

        }

        void Update()
        {
            if (Disable)
            {
                return;
            }
            gridInput?.Tick();
        }
    }
}
