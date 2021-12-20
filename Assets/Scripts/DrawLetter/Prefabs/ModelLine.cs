namespace MagicLetters.DrawLetter.Prefabs
{
    using MagicLetters.DrawLetter.Helpers;
    using MagicLetters.DrawLetter.Structs.Args;
    using UnityEngine;

    public class ModelLine : MonoBehaviour
    {
        public LineRenderer LineRenderer;
        public EdgeCollider2D EdgeCollider2D;


        public void Draw(DrawLineArgs args)
        {
            gameObject.transform.SetParent(args.ParentObj.transform);
            LineRenderer.useWorldSpace = true;

            LineRenderer.positionCount = args.Points.Count;
            LineRenderer.SetPositions(Helper.Vector2ToVector3(args.Points).ToArray());
            EdgeCollider2D.points = args.Points.ToArray();
        }


        public Vector2? CheckMousePosition(Vector2 pos, float distance)
        {
            Vector2? result = null;
            Vector2 closestPoint = EdgeCollider2D.ClosestPoint(pos);
            if (Vector3.Distance(closestPoint, pos) <= distance)
            {
                result = closestPoint;
            }


            return result;
        }


    }
}
