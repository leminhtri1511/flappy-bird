using UnityEngine;
using UnityEngine.UI;

namespace FPB.Scripts
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private CanvasScaler _canvasScaler;

        private Camera _camera;

        private void Awake()
        {
            _camera = _canvas.worldCamera;

            ScreenSizeCalculate();
        }

        private void ScreenSizeCalculate()
        {
            var isTablet = _camera.aspect > (9f / 16f);


            _canvasScaler.matchWidthOrHeight = isTablet ? 1f : 0;
        }
    }
}