namespace MagicLetters.DrawLetter.Structs
{
    using MagicLetters.DrawLetter.Prefabs;
    using UnityEngine;

    public struct LineToAnalyse
    {
        public Vector2 StartPoint;
        public Vector2 EndPoint;
        public bool IsDrawingStarted;
        public bool IsDrawn;
        public EdgeCollider2D ModelEdgeCollider2D;
        public UserLine UserLine;
        public LetterNode Node;
    }
}