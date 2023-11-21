using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GridSystem
{
    public sealed class RaycastGridInput : IGridInput
    {
        public Action<GridCell> OnInput { get; set; }

        private IGridController gridController;

        private float distance;
        private string gridLayerName;
        private Camera cam;
        private Ray ray;

        public RaycastGridInput(IGridController gridController, string gridLayerName, float distance)
        {
            this.gridController = gridController;
            this.distance = distance;
            this.gridLayerName = gridLayerName;
            cam = Camera.main;
        }

        public void Tick()
        {

            Vector3 mousePosition = Input.mousePosition;

            OnScreenRaycastUpdate(GetPointPosition());
        }

        private Vector3 GetPointPosition()
        {
            Vector3 mousePosition = Vector3.zero;
#if ENABLE_INPUT_SYSTEM
    #if UNITY_EDITOR
            var inputMousePos = Mouse.current.position;
            mousePosition = new Vector3(inputMousePos.x.value, inputMousePos.y.value, 0f);
            return mousePosition;

    #endif
    #if UNITY_ANDROID
            var inputTouchPos = Touchscreen.current.position;
    #else
                var inputTouchPos = Mouse.current.position;
    #endif
            return new Vector3(inputTouchPos.x.value, inputTouchPos.y.value, 0f);

#else
            mousePosition = Input.mousePosition;
#endif
            return mousePosition;
        }

        private void OnScreenRaycastUpdate(Vector3 mousePos)
        {
            if (gridController == null)
            {
                return;
            }
            ray = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out var hit, distance, 1 << LayerMask.NameToLayer(gridLayerName)))
            {
                Vector3 point = hit.point;
                point.y -= gridController.Grid.Cells[0].Size.y / 2f;
                point.y = 0f;
                gridController.CheckPosition(point, OnInput);
            }
        }
    }

}
