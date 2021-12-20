namespace MagicLetters.DrawLetter.ScriptableObjects
{
    using System.Collections.Generic;
    using MagicLetters.DrawLetter.Structs;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Letter", menuName = "Letters/New Letter", order = 1)]
    public class LetterAttributes : ScriptableObject
    {
        public List<Stroke> strokes;
    }
}