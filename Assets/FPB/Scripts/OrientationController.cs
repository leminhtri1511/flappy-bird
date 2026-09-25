using UnityEngine;

namespace FPB.Scripts
{
    public class OrientationController : MonoBehaviour
    {
        private void Awake()
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }
}