namespace MagicLetters.DrawLetter.Helpers
{
    using System.Collections.Generic;
    using UnityEngine;


    public class Helper
    {
        public static List<Vector3> Vector2ToVector3(List<Vector2> vector2s)
        {
            List<Vector3> result = new List<Vector3>();
            foreach (Vector2 vector2 in vector2s)
            {
                Vector3 vector3 = vector2;
                result.Add(vector3);
            }
            return result;
        }


    }
}


