namespace MagicLetters.DrawLetter.Structs.Args
{
    using MagicLetters.DrawLetter.Prefabs;
    using MagicLetters.DrawLetter.ScriptableObjects;
    using UnityEngine;

    public struct UserLetterArgs
    {
        public LetterAttributes LetterAttributes;
        public UserLine Prefab;
        public GameObject Container;
        public ModelLetter ModelLetterObj;
        public float Offset;
    }
}