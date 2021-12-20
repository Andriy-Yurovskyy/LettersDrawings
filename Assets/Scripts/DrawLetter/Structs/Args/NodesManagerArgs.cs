namespace MagicLetters.DrawLetter.Structs.Args
{
    using MagicLetters.DrawLetter.Prefabs;
    using MagicLetters.DrawLetter.ScriptableObjects;
    using UnityEngine;

    public struct NodesManagerArgs
    {
        public LetterAttributes LetterAttributes;
        public LetterNode Prefab;
        public GameObject Container;
        public RectTransform CanvasRect;
        public Camera Cam;
    }
}