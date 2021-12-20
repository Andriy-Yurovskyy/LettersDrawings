namespace MagicLetters.Touches
{
    using UnityEngine;

    public class TouchAnalyzer : MonoBehaviour
    {
        [SerializeField]
        Camera cam;
        private static Vector2? pos;


        void Update()
        {
            if (!Input.GetMouseButton(0))
            {
                pos = null;
                return;
            }
            pos = cam.ScreenToWorldPoint(Input.mousePosition);
        }


        public static Vector2? GetTouchPos()
        {
            return pos;
        }


    }

}
