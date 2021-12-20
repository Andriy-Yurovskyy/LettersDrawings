namespace MagicLetters.DrawLetter.Structs.Args
{
    using MagicLetters.DrawLetter.Prefabs;
    using MagicLetters.DrawLetter.ScriptableObjects;
    using UnityEngine;

    public struct ModelLetterArgs
    {
        public LetterAttributes LetterAttributes;
        public ModelLine Prefab;
        public GameObject Container;
    }
}