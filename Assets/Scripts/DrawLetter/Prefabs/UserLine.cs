namespace MagicLetters.DrawLetter.Prefabs
{
    using System.Collections.Generic;
    using UnityEngine;

    public class UserLine : MonoBehaviour
    {
        public LineRenderer LineRenderer;

        private float lineZValue = -4;

        private List<Vector3> currentPositions;


        private void Start()
        {
            currentPositions = new List<Vector3>();
        }


        public void Draw(Vector2 point)
        {
            LineRenderer.useWorldSpace = true;
            LineRenderer.positionCount++;
            currentPositions.Add(new Vector3(point.x, point.y, lineZValue));
            LineRenderer.SetPositions(currentPositions.ToArray());
        }

        public void Erase()
        {
            LineRenderer.positionCount = 0;
            LineRenderer.SetPositions(new Vector3[0]);
            currentPositions.Clear();
        }


    }
}