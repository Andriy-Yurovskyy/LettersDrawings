namespace MagicLetters.DrawLetter.Structs.Args
{
    using System.Collections.Generic;
    using UnityEngine;

    public struct DrawLineArgs
    {
        public GameObject ParentObj;
        public bool DrawCollider;
        public List<Vector2> Points;
    }
}